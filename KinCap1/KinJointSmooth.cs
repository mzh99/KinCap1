using System;
using Microsoft.Kinect;
using System.Numerics;

namespace KinCap1 {

   public class TransSmoothParms {
      public float Smoothing { get; set; }            // [0..1], lower values closer to raw data
      public float Correction { get; set; }           // [0..1], lower values slower to correct towards the raw data
      public float Prediction { get; set; }           // [0..n], the number of frames to predict into the future
      public float JitterRadius { get; set; }         // The radius in meters for jitter reduction
      public float MaxDeviationRadius { get; set; }   // The maximum radius in meters that filtered positions are allowed to deviate from raw data
   }

   /// <summary>Implementation of a Holt Double Exponential Smoothing filter. Smooths the curve, with jitter removal</summary>
   public class JointsDoubleExpFilter {

      /// <summary>Historical Filter Data./// </summary>
      private struct FilterDoubleExponentialData {
         /// <summary>Historical Position.</summary>
         public Vector3 RawPosition { get; set; }
         /// <summary>Historical Filtered Position.</summary>
         public Vector3 FilteredPosition { get; set; }
         /// <summary>Historical Trend.</summary>
         public Vector3 Trend { get; set; }
         /// <summary>Historical FrameCount.</summary>
         public uint FrameCount { get; set; }
      }

      /// <summary>The previous data.</summary>
      private FilterDoubleExponentialData[] history;
      /// <summary>The transform smoothing parameters for this filter.</summary>
      private TransSmoothParms smoothParameters;

      /// <summary>Initializes a new instance of the <see cref="JointsDoubleExpFilter"/> class.</summary>
      public JointsDoubleExpFilter() {
         Init(0.25f, 0.25f, 0.25f, 0.75f, 1.2f);
      }

      public JointsDoubleExpFilter(float smoothingValue, float correctionValue, float predictionValue, float jitterRadiusValue, float maxDeviationRadiusValue) {
         Init(smoothingValue, correctionValue, predictionValue, jitterRadiusValue, maxDeviationRadiusValue);
      }

      /// <summary>Initialize the filter with a set of manually specified TransSmoothParms.</summary>
      /// <param name="smoothingValue">Smoothing = [0..1], lower values is closer to the raw data and more noisy.</param>
      /// <param name="correctionValue">Correction = [0..1], higher values correct faster and feel more responsive.</param>
      /// <param name="predictionValue">Prediction = [0..n], how many frames into the future we want to predict.</param>
      /// <param name="jitterRadiusValue">JitterRadius = The deviation distance in m that defines jitter.</param>
      /// <param name="maxDeviationRadiusValue">MaxDeviation = The maximum distance in m that filtered positions are allowed to deviate from raw data.</param>
      public void Init(float smoothingValue, float correctionValue, float predictionValue, float jitterRadiusValue, float maxDeviationRadiusValue) {
         smoothParameters = new TransSmoothParms {
            MaxDeviationRadius = maxDeviationRadiusValue, // Size of the max prediction radius Can snap back to noisy data when too high
            Smoothing = smoothingValue,                   // How much soothing will occur.  Will lag when too high
            Correction = correctionValue,                 // How much to correct back from prediction.  Can make things springy
            Prediction = predictionValue,                 // Amount of prediction into the future to use. Can over shoot when too high
            JitterRadius = jitterRadiusValue             // Size of the radius where jitter is removed. Can do too much smoothing when too high
         };
         Array jointTypeValues = Enum.GetValues(typeof(JointType));
         history = new FilterDoubleExponentialData[jointTypeValues.Length];

      }

      /// <summary>Update the filter with a new frame of data and smooth.</summary>
      public void UpdateFilter(ref KinCapFrame CapFrame) {

         Array jointTypeValues = Enum.GetValues(typeof(JointType));
         TransSmoothParms tempSmoothingParams = new TransSmoothParms();
         smoothParameters.JitterRadius = Math.Max(0.0001f, smoothParameters.JitterRadius);
         tempSmoothingParams.Smoothing = smoothParameters.Smoothing;
         tempSmoothingParams.Correction = smoothParameters.Correction;
         tempSmoothingParams.Prediction = smoothParameters.Prediction;
         foreach (JointType jt in jointTypeValues) {
            // If not tracked, we smooth a bit more by using a bigger jitter radius
            // Always filter feet highly as they are so noisy
            int jNdx = (int) jt;
            if ((CapFrame.TrackState[jNdx] == TrackingState.Tracked) && (IsFeet(jt) == false)) {
               tempSmoothingParams.JitterRadius = smoothParameters.JitterRadius;
               tempSmoothingParams.MaxDeviationRadius = smoothParameters.MaxDeviationRadius;
            }
            else {
               tempSmoothingParams.JitterRadius *= 2.0f;
               tempSmoothingParams.MaxDeviationRadius *= 2.0f;
            }
            FilterJoint(ref CapFrame, jNdx, tempSmoothingParams);
         }
      }

      private bool IsFeet(JointType jointID) {
         return (jointID == JointType.AnkleLeft) || (jointID == JointType.AnkleRight) || (jointID == JointType.FootLeft) || (jointID == JointType.FootRight);

      }

      /// <summary>Update the filter for one joint.</summary>
      /// <param name="skeleton">The Skeleton to filter.</param>
      /// <param name="jt">The Skeleton Joint index to filter.</param>
      /// <param name="smoothingParameters">The Smoothing parameters to apply.</param>
      protected void FilterJoint(ref KinCapFrame capFrame, int jt, TransSmoothParms smoothingParameters) {

         Vector3 filteredPosition;
         Vector3 diffvec;
         Vector3 trend;
         float diffVal;

         Vector3 rawPosition = new Vector3(capFrame.SmoothedCSP[jt].X, capFrame.SmoothedCSP[jt].Y, capFrame.SmoothedCSP[jt].Z);
         Vector3 prevFilteredPosition = history[jt].FilteredPosition;
         Vector3 prevTrend = history[jt].Trend;
         Vector3 prevRawPosition = history[jt].RawPosition;

         // If joint is invalid, reset the filter
         if ((capFrame.SmoothedCSP[jt].X == 0.0f) && (capFrame.SmoothedCSP[jt].Y == 0.0f) & (capFrame.SmoothedCSP[jt].Z == 0.0f)) {
            history[jt].FrameCount = 0;
         }

         // Initial start values
         if (history[jt].FrameCount == 0) {
            filteredPosition = rawPosition;
            trend = Vector3.Zero;
         }
         else if (history[jt].FrameCount == 1) {
            filteredPosition = Vector3.Multiply(Vector3.Add(rawPosition, prevRawPosition), 0.5f);
            diffvec = Vector3.Subtract(filteredPosition, prevFilteredPosition);
            trend = Vector3.Add(Vector3.Multiply(diffvec, smoothingParameters.Correction), Vector3.Multiply(prevTrend, 1.0f - smoothingParameters.Correction));
         }
         else {
            // First apply jitter filter
            diffvec = Vector3.Subtract(rawPosition, prevFilteredPosition);
            diffVal = Math.Abs(diffvec.Length());

            if (diffVal <= smoothingParameters.JitterRadius) {
               filteredPosition = Vector3.Add(Vector3.Multiply(rawPosition, diffVal / smoothingParameters.JitterRadius), Vector3.Multiply(prevFilteredPosition, 1.0f - (diffVal / smoothingParameters.JitterRadius)));
            }
            else {
               filteredPosition = rawPosition;
            }

            // Now the double exponential smoothing filter
            filteredPosition = Vector3.Add(Vector3.Multiply(filteredPosition, 1.0f - smoothingParameters.Smoothing), Vector3.Multiply(Vector3.Add(prevFilteredPosition, prevTrend), smoothingParameters.Smoothing));

            diffvec = Vector3.Subtract(filteredPosition, prevFilteredPosition);
            trend = Vector3.Add(Vector3.Multiply(diffvec, smoothingParameters.Correction), Vector3.Multiply(prevTrend, 1.0f - smoothingParameters.Correction));
         }

         // Predict into the future to reduce latency
         Vector3 predictedPosition = Vector3.Add(filteredPosition, Vector3.Multiply(trend, smoothingParameters.Prediction));

         // Check that we are not too far away from raw data
         diffvec = Vector3.Subtract(predictedPosition, rawPosition);
         diffVal = Math.Abs(diffvec.Length());

         if (diffVal > smoothingParameters.MaxDeviationRadius) {
            predictedPosition = Vector3.Add(Vector3.Multiply(predictedPosition, smoothingParameters.MaxDeviationRadius / diffVal), Vector3.Multiply(rawPosition, 1.0f - (smoothingParameters.MaxDeviationRadius / diffVal)));
         }

         // Save the data from this frame
         history[jt].RawPosition = rawPosition;
         history[jt].FilteredPosition = filteredPosition;
         history[jt].Trend = trend;
         history[jt].FrameCount++;

         // Set the filtered data back into the joint
         capFrame.SmoothedCSP[jt].X = predictedPosition.X;
         capFrame.SmoothedCSP[jt].Y = predictedPosition.Y;
         capFrame.SmoothedCSP[jt].Z = predictedPosition.Z;
      }
   }
}
