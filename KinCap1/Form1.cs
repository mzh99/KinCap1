using System;
using System.Threading;
using System.IO;
using System.Numerics;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.Kinect;

namespace KinCap1 {

   public partial class Main_Frm: Form {

      const int MaxFrames = 10000;

      private KinSkeleTrack skele = null;
      private readonly KinCapFrame[] capFrames = new KinCapFrame[MaxFrames];

      // Total frames captured. This can differ from Skele.FrameCnt since Kinect frames are all bodies. We can OnFrame once per body within a frame
      private int frameTotCnt;

      public Main_Frm() {
         InitializeComponent();
         InitCapFrames();
         MelSavePath_TB.Text = GetAppPathDocuments();
      }

      private string GetAppPathDocuments() {
         return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Path.DirectorySeparatorChar;
      }

      private void InitCapFrames() {
         // init buffers for frames
         for (int z = 0; z < MaxFrames; z++) {
            capFrames[z] = new KinCapFrame();
         }
      }

      private void StartBtnOnClick(object sender, EventArgs e) {
         frameTotCnt = 0;
         // wait 10 seconds with countdown
         for (int cnt = 10; cnt >= 1; cnt--) {
            Frame_Lbl.Text = "Countdown: " + cnt.ToString();
            Frame_Lbl.Refresh();
            Thread.Sleep(1000);
         }
         Frame_Lbl.Text = "Starting capture";
         skele = new KinSkeleTrack();
         if (skele == null) {
            ErrMsg("Cannot get skeleton for capture");
            return;
         }
         skele.OnAvail += Skele_OnAvail;
         skele.OnFrame += Skele_OnFrame;
         skele.StartTracking();
      }

      private void Stop_Btn_Click(object sender, EventArgs e) {
         skele.StopTracking();
         skele.OnAvail -= Skele_OnAvail;
         skele.OnFrame -= Skele_OnFrame;
         Frame_Lbl.Text = string.Format("Total Frames: {0}", frameTotCnt);
         LoadJointList();
      }

      private void Plot(int Jt) {

         Unsmooth_Cht.Series.Clear();
         Series SeriesX = Unsmooth_Cht.Series.Add("X");
         SeriesX.ChartType = SeriesChartType.Line;
         Series SeriesY = Unsmooth_Cht.Series.Add("Y");
         SeriesY.ChartType = SeriesChartType.Line;
         Series SeriesZ = Unsmooth_Cht.Series.Add("Z");
         SeriesZ.ChartType = SeriesChartType.Line;

         Smooth_Cht.Series.Clear();
         Series SmSeriesX = Smooth_Cht.Series.Add("X");
         SmSeriesX.ChartType = SeriesChartType.Line;
         Series SmSeriesY = Smooth_Cht.Series.Add("Y");
         SmSeriesY.ChartType = SeriesChartType.Line;
         Series SmSeriesZ = Smooth_Cht.Series.Add("Z");
         SmSeriesZ.ChartType = SeriesChartType.Line;

         for (int z = 0; z < frameTotCnt; z++) {
            if (capFrames[z].BodyNdx == 0) {    // first body
               SeriesX.Points.Add(capFrames[z].CamPos[Jt].X);
               SeriesY.Points.Add(capFrames[z].CamPos[Jt].Y);
               SeriesZ.Points.Add(capFrames[z].CamPos[Jt].Z);

               SmSeriesX.Points.Add(capFrames[z].SmoothedCSP[Jt].X);
               SmSeriesY.Points.Add(capFrames[z].SmoothedCSP[Jt].Y);
               SmSeriesZ.Points.Add(capFrames[z].SmoothedCSP[Jt].Z);
            }
         }
      }

      private void LoadJointList() {

         JointList_CB.Items.Clear();
         Array jointTypeValues = Enum.GetValues(typeof(JointType));
         foreach (JointType jt in jointTypeValues) {
            JointList_CB.Items.Add(jt.ToString());
         }
      }

      private void SaveMel_Btn_Click(object sender, EventArgs e) {
         string savePath = string.IsNullOrEmpty(MelSavePath_TB.Text) ? GetAppPathDocuments() : MelSavePath_TB.Text;
         if (Directory.Exists(savePath) == false) {
            ErrMsg("Save path for Mel scripts is invalid");
            return;
         }
         string melNormalFilename = Path.Combine(savePath, "mel.txt");
         string melSmoothedFilename = Path.Combine(savePath, "melSmoothed.txt");
         SaveJointDataToMEL(false, melNormalFilename);
         SaveJointDataToMEL(true, melSmoothedFilename);
         Frame_Lbl.Text = "MEL scripts saved";
      }

      /// <summary>Save Joint Data to Mel script for testing</summary>
      /// <param name="UseSmoothedData">True to use smoothed data</param>
      /// <param name="filename">MEL File name to save as</param>
      private void SaveJointDataToMEL(bool UseSmoothedData, string filename) {
         CameraSpacePoint camPos;
         StringBuilder sb = new StringBuilder();
         // setup Polyspheres for each joint
         sb.AppendLine("// Setup spheres for each joint");
         foreach (JointType JT in Enum.GetValues(typeof(JointType))) {
            if ((JT == JointType.FootLeft) || (JT == JointType.FootRight) || (JT == JointType.HandTipLeft) || (JT == JointType.HandTipRight)) {
               sb.AppendLine(string.Format("polySphere -r 0.01 -sx 8 -sy 8 -ax 0 1 0 -cuv 2 -ch 1 -n {0};", JT));
            }
            else {
               sb.AppendLine(string.Format("polySphere -r 0.02 -sx 8 -sy 8 -ax 0 1 0 -cuv 2 -ch 1 -n {0};", JT));
            }
         }
         sb.AppendLine("// end of Polysphere setup");

         // constrain skele to polyspheres
         sb.AppendLine("// Setup constraints for each joint");
         foreach (JointType JT in Enum.GetValues(typeof(JointType))) {
            sb.AppendLine(string.Format("pointConstraint {0} sk_{0};", JT));
         }
         sb.AppendLine("// end of constraint setup");

         if (IncShaders_CB.Checked == true) {
            // Setup automatic Shader creation code as a preamble for the animation
            // Each joint needs its own shader so we can show color for each joint
            sb.AppendLine("// Setup Shaders for each joint");
            foreach (JointType JT in Enum.GetValues(typeof(JointType))) {
               sb.AppendLine(string.Format("shadingNode -asShader lambert -name {0}_shader;", JT));
               sb.AppendLine(string.Format("sets -renderable true -noSurfaceShader true -empty -name {0}_SG;", JT));
               sb.AppendLine(string.Format("connectAttr -f {0}_shader.outColor {0}_SG.surfaceShader;", JT));
               sb.AppendLine(string.Format("sets -forceElement {0}_SG {0};", JT));
            }
            sb.AppendLine("// end of Shader Setup");
         }

         for (int z = 0; z < frameTotCnt; z++) {
            if (capFrames[z].BodyNdx == 0) {    // only capture first body
               sb.AppendLine(string.Format("currentTime {0};", capFrames[z].FrameNum));    // use kinect frame# which shares across multiple bodies if in the same scene
               Matrix4x4 FCPMatrix = FCPCamTransform(capFrames[z].FloorClip);   // create a transformation matrix for the floor clip plane (which is tilted)
               for (int y = 0; y <= capFrames[z].CamPos.GetUpperBound(0); y++) {
                  if (UseSmoothedData == true) {
                     camPos = capFrames[z].SmoothedCSP[y];
                  }
                  else {
                     camPos = capFrames[z].CamPos[y];
                  }
                  Vector3 InV = new Vector3(camPos.X, camPos.Y, camPos.Z);      // convert to vector
                  InV = Vector3.Transform(InV, FCPMatrix);      // correct frame with floor clip plane
                  // assign back to CameraSpacePoint structure
                  camPos.X = InV.X;
                  camPos.Y = InV.Y;
                  camPos.Z = InV.Z;
                  JointType jointType = (JointType) y;   // cast back from integer to enum
                  if (capFrames[z].TrackState[y] == TrackingState.Tracked) {
                     sb.AppendLine(string.Format("setAttr \"{0}.translateX\" {1};", jointType, camPos.X));
                     sb.AppendLine(string.Format("setAttr \"{0}.translateY\" {1};", jointType, camPos.Y));
                     sb.AppendLine(string.Format("setAttr \"{0}.translateZ\" {1};", jointType, camPos.Z));
                     sb.AppendLine(string.Format("setKeyframe (\"{0}.t\");", jointType));
                     if (IncShaders_CB.Checked == true) {
                        // set color for joint shader
                        sb.AppendLine(string.Format("setAttr \"{0}_shader.color\" -type double3 0 1 0;", jointType));
                        sb.AppendLine(string.Format("setKeyframe ( \"{0}_shader.c\" );", jointType));
                     }
                  }
                  else {
                     if (capFrames[z].TrackState[y] == TrackingState.Inferred) {
                        sb.AppendLine("// Inferred Joint");
                        if (IncInferred_CB.Checked == true) {
                           sb.AppendLine(string.Format("setAttr \"{0}.translateX\" {1};", jointType, camPos.X));
                           sb.AppendLine(string.Format("setAttr \"{0}.translateY\" {1};", jointType, camPos.Y));
                           sb.AppendLine(string.Format("setAttr \"{0}.translateZ\" {1};", jointType, camPos.Z));
                           sb.AppendLine(string.Format("setKeyframe (\"{0}.t\");", jointType));
                           if (IncShaders_CB.Checked == true) {
                              // set color for joint shader
                              sb.AppendLine(string.Format("setAttr \"{0}_shader.color\" -type double3 1 1 0;", jointType));
                              sb.AppendLine(string.Format("setKeyframe ( \"{0}_shader.c\" );", jointType));
                           }
                        }
                        else {
                           sb.AppendLine(string.Format("// setAttr \"{0}.translateX\" {1};", jointType, camPos.X));
                           sb.AppendLine(string.Format("// setAttr \"{0}.translateY\" {1};", jointType, camPos.Y));
                           sb.AppendLine(string.Format("// setAttr \"{0}.translateZ\" {1};", jointType, camPos.Z));
                           sb.AppendLine(string.Format("// setKeyframe (\"{0}.t\");", jointType));
                           if (IncShaders_CB.Checked == true) {
                              // set color for joint shader
                              sb.AppendLine(string.Format("// setAttr \"{0}_shader.color\" -type double3 1 1 0;", jointType));
                              sb.AppendLine(string.Format("// setKeyframe ( \"{0}_shader.c\" );", jointType));
                           }
                        }
                     }
                  }
               }
            }
         }
         File.WriteAllText(filename, sb.ToString());
      }

      private void Skele_OnFrame(KinCapFrame OneFrame) {

         if (frameTotCnt < MaxFrames) {
            if (OneFrame.BodyNdx == 0) {     // we're only grabbing body 0
               // copy frame data including joints, positions, tracking, etc
               capFrames[frameTotCnt].BodyNdx = OneFrame.BodyNdx;
               capFrames[frameTotCnt].FloorClip = OneFrame.FloorClip;
               capFrames[frameTotCnt].TmSpan = OneFrame.TmSpan;
               capFrames[frameTotCnt].FrameNum = OneFrame.FrameNum;
               for (int z = 0; z <= OneFrame.CamPos.GetUpperBound(0); z++) {
                  capFrames[frameTotCnt].CamPos[z] = OneFrame.CamPos[z];
                  capFrames[frameTotCnt].SmoothedCSP[z] = OneFrame.SmoothedCSP[z];
                  capFrames[frameTotCnt].TrackState[z] = OneFrame.TrackState[z];
               }

               if (frameTotCnt % 10 == 0) {
                  Frame_Lbl.Text = string.Format("Frames: {0}", frameTotCnt);
               }
               frameTotCnt++;
            }
         }
         else {
            Frame_Lbl.Text = "Max Frames hit!";
            Frame_Lbl.Refresh();
         }
      }

      private void Skele_OnAvail(bool IsAvail) {
         StatusBar.Text = IsAvail ? "Available" : "Not Available";
      }

      // 11/29/20 changed from old XNA library to standard .net library for matrix math and vectors
      private Matrix4x4 FCPCamTransform(Microsoft.Kinect.Vector4 normal) {
         // use floor clip plane to transform Camera points into World points

         Vector3 yNew = new Vector3(normal.X, normal.Y, normal.Z);
         Vector3 zNew = Vector3.Normalize(new Vector3(0, 1, -normal.Y / normal.Z));
         Vector3 xNew = Vector3.Cross(yNew, zNew);

         // assumes column vectors, multiplied on the right
         Matrix4x4 rotMatrix = new Matrix4x4(
             xNew.X, yNew.X, zNew.X, 0,
             xNew.Y, yNew.Y, zNew.Y, 0,
             xNew.Z, yNew.Z, zNew.Z, 0,
             0, 0, 0, 1);

         Matrix4x4 transMatrix = new Matrix4x4(
               1, 0, 0, 0,
               0, 1, 0, -normal.W,
               0, 0, 1, 0,
               0, 0, 0, 1);

         return rotMatrix * transMatrix;
      }

      private void JointList_CB_SelectedIndexChanged(object sender, EventArgs e) {
         if (JointList_CB.SelectedIndex >= 0) {
            Plot(JointList_CB.SelectedIndex);
         }
      }

      private void Smooth_Btn_Click(object sender, EventArgs e) {
         float smoothingVal, correctionVal, predictionVal, jitterVal, maxDevVal;

         if (float.TryParse(Smooth_Parm_EB.Text, out smoothingVal) == false) {
            ErrMsg("Smoothing value is not a valid floating point number.");
            return;
         }
         if (float.TryParse(Correction_Parm_EB.Text, out correctionVal) == false) {
            ErrMsg("Correction value is not a valid floating point number.");
            return;
         }
         if (float.TryParse(Pred_Parm_EB.Text, out predictionVal) == false) {
            ErrMsg("Prediction value is not a valid floating point number.");
            return;
         }
         if (float.TryParse(Jitter_Parm_EB.Text, out jitterVal) == false) {
            ErrMsg("Jitter value is not a valid floating point number.");
            return;
         }
         if (float.TryParse(MaxDev_Parm_EB.Text, out maxDevVal) == false) {
            ErrMsg("Max Dev value is not a valid floating point number.");
            return;
         }
         ResetSmoothedJoints();  // reset smoothed joint data back to original
         JointsDoubleExpFilter SF = new JointsDoubleExpFilter(smoothingVal, correctionVal, predictionVal, jitterVal, maxDevVal);
         for (int z = 0; z < frameTotCnt; z++) {
            if (capFrames[z].BodyNdx == 0) {    // first body
               SF.UpdateFilter(ref capFrames[z]);
            }
         }
         if (JointList_CB.SelectedIndex >= 0) {
            Plot(JointList_CB.SelectedIndex);
         }
      }

      /// <summary>Reset smoothed joint data back to original capture</summary>
      private void ResetSmoothedJoints() {
         for (int z = 0; z < frameTotCnt; z++) {
            for (int y = 0; y <= capFrames[z].CamPos.GetUpperBound(0); y++) {
               capFrames[z].SmoothedCSP[y].X = capFrames[z].CamPos[y].X;
               capFrames[z].SmoothedCSP[y].Y = capFrames[z].CamPos[y].Y;
               capFrames[z].SmoothedCSP[y].Z = capFrames[z].CamPos[y].Z;
            }
         }

      }

      private void ErrMsg(string errMsg) {
         MessageBox.Show(errMsg, "Error", MessageBoxButtons.OK);
      }

   }

}
