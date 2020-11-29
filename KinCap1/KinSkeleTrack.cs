using System;
using System.Collections.Generic;
using Microsoft.Kinect;

namespace KinCap1 {

   /// <summary>Class for Kinect frame data</summary>
   public class KinCapFrame {

      const int MAXJOINTS = 25;

      public int FrameNum { get; set; }
      public int BodyNdx { get; set; }
      public TimeSpan TmSpan { get; set; }
      public Vector4 FloorClip { get; set; }
      public CameraSpacePoint[] CamPos { get; set; }
      public CameraSpacePoint[] SmoothedCSP { get; set; }
      public TrackingState[] TrackState { get; set; }

      public KinCapFrame() {
         CamPos = new CameraSpacePoint[MAXJOINTS];
         SmoothedCSP = new CameraSpacePoint[MAXJOINTS];
         TrackState = new TrackingState[MAXJOINTS];
      }

   }

   /// <summary>Kinect Skeleton Tracking of joint data</summary>
   public class KinSkeleTrack {

      private KinectSensor kSensor = null;

      /// <summary>Reader for body frames</summary>
      private BodyFrameReader bodyFrmRdr = null;

      /// <summary>Array for the bodies</summary>
      private Body[] bodies = null;

      private IReadOnlyDictionary<JointType, Joint> BJoints = null;

      private readonly KinCapFrame Frm = null;

      public delegate void FrameHandler(KinCapFrame OneFrame);
      public event FrameHandler OnFrame;

      public delegate void AvailHandler(bool IsAvail);
      public event AvailHandler OnAvail;

      public int FrameCnt { get; private set; }

      public KinSkeleTrack() {
         Frm = new KinCapFrame();
      }

      public void StartTracking() {

         FrameCnt = 0;

         // one sensor is currently supported
         kSensor = KinectSensor.GetDefault();

         // open the reader for the body frames
         bodyFrmRdr = kSensor.BodyFrameSource.OpenReader();
         // setup Framearrived event
         if (bodyFrmRdr != null)
            bodyFrmRdr.FrameArrived += Reader_FrameArrived;

         // set IsAvailableChanged event notifier
         kSensor.IsAvailableChanged += Sensor_IsAvailableChanged;
         kSensor.Open();
      }

      public void StopTracking() {

         if (bodyFrmRdr != null) {
            bodyFrmRdr.Dispose();      // BodyFrameReader is IDisposable
            bodyFrmRdr = null;
         }

         if (kSensor != null) {
            kSensor.Close();
            kSensor = null;
         }

      }

      private void Sensor_IsAvailableChanged(object sender, IsAvailableChangedEventArgs e) {
         OnAvail?.Invoke(e.IsAvailable);
      }

      /// <summary>Handles the body frame data arriving from the sensor</summary>
      /// <param name="sender">object sending the event</param>
      /// <param name="e">event arguments</param>
      private void Reader_FrameArrived(object sender, BodyFrameArrivedEventArgs e) {

         using (BodyFrame BFrame = e.FrameReference.AcquireFrame()) {
            if (BFrame != null) {
               if (bodies == null) {
                  bodies = new Body[BFrame.BodyCount];
               }
               Frm.TmSpan = e.FrameReference.RelativeTime;
               Frm.FloorClip = BFrame.FloorClipPlane;
               Frm.FrameNum = FrameCnt;

               // The first time GetAndRefreshBodyData is called, Kinect will allocate each Body in the array.
               // As long as those body objects are not disposed and not set to null in the array, those body objects will be reused.
               BFrame.GetAndRefreshBodyData(bodies);
               bool FrameHasTrackedBodies = false;
               for (int BNdx = 0; BNdx < bodies.Length; BNdx++) {
                  if (bodies[BNdx].IsTracked == true) {
                     FrameHasTrackedBodies = true;
                     Frm.BodyNdx = BNdx;
                     BJoints = bodies[BNdx].Joints;
                     foreach (JointType JType in BJoints.Keys) {
                        Frm.CamPos[(int) JType] = BJoints[JType].Position;
                        Frm.SmoothedCSP[(int) JType] = BJoints[JType].Position;     // overwritten if smoothing is called later
                        Frm.TrackState[(int) JType] = BJoints[JType].TrackingState;
                     }
                     OnFrame?.Invoke(Frm);
                  }
               }
               if (FrameHasTrackedBodies == true) {
                  FrameCnt++;
               }
            }
         }
      }

   }

}
