namespace KinCap1 {
   partial class Main_Frm {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing) {
         if (disposing && (components != null)) {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent() {
         System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
         System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
         System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
         System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
         System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
         System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
         this.Start_Btn = new System.Windows.Forms.Button();
         this.Stop_Btn = new System.Windows.Forms.Button();
         this.StatusBar = new System.Windows.Forms.StatusStrip();
         this.Frame_Lbl = new System.Windows.Forms.Label();
         this.SaveMel_Btn = new System.Windows.Forms.Button();
         this.IncInferred_CB = new System.Windows.Forms.CheckBox();
         this.IncShaders_CB = new System.Windows.Forms.CheckBox();
         this.JointList_CB = new System.Windows.Forms.ComboBox();
         this.Unsmooth_Cht = new System.Windows.Forms.DataVisualization.Charting.Chart();
         this.Smooth_Cht = new System.Windows.Forms.DataVisualization.Charting.Chart();
         this.label1 = new System.Windows.Forms.Label();
         this.Smooth_Parm_EB = new System.Windows.Forms.TextBox();
         this.Correction_Parm_EB = new System.Windows.Forms.TextBox();
         this.label2 = new System.Windows.Forms.Label();
         this.Pred_Parm_EB = new System.Windows.Forms.TextBox();
         this.label3 = new System.Windows.Forms.Label();
         this.Jitter_Parm_EB = new System.Windows.Forms.TextBox();
         this.label4 = new System.Windows.Forms.Label();
         this.MaxDev_Parm_EB = new System.Windows.Forms.TextBox();
         this.label5 = new System.Windows.Forms.Label();
         this.Smooth_Btn = new System.Windows.Forms.Button();
         this.label6 = new System.Windows.Forms.Label();
         this.MelSavePath_TB = new System.Windows.Forms.TextBox();
         ((System.ComponentModel.ISupportInitialize)(this.Unsmooth_Cht)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.Smooth_Cht)).BeginInit();
         this.SuspendLayout();
         // 
         // Start_Btn
         // 
         this.Start_Btn.Location = new System.Drawing.Point(22, 13);
         this.Start_Btn.Margin = new System.Windows.Forms.Padding(4);
         this.Start_Btn.Name = "Start_Btn";
         this.Start_Btn.Size = new System.Drawing.Size(119, 36);
         this.Start_Btn.TabIndex = 10;
         this.Start_Btn.Text = "Start";
         this.Start_Btn.UseVisualStyleBackColor = true;
         this.Start_Btn.Click += new System.EventHandler(this.StartBtnOnClick);
         // 
         // Stop_Btn
         // 
         this.Stop_Btn.Location = new System.Drawing.Point(162, 13);
         this.Stop_Btn.Margin = new System.Windows.Forms.Padding(4);
         this.Stop_Btn.Name = "Stop_Btn";
         this.Stop_Btn.Size = new System.Drawing.Size(119, 36);
         this.Stop_Btn.TabIndex = 12;
         this.Stop_Btn.Text = "Stop";
         this.Stop_Btn.UseVisualStyleBackColor = true;
         this.Stop_Btn.Click += new System.EventHandler(this.Stop_Btn_Click);
         // 
         // StatusBar
         // 
         this.StatusBar.Location = new System.Drawing.Point(0, 812);
         this.StatusBar.Name = "StatusBar";
         this.StatusBar.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
         this.StatusBar.Size = new System.Drawing.Size(1160, 22);
         this.StatusBar.TabIndex = 3;
         this.StatusBar.Text = "My info here";
         // 
         // Frame_Lbl
         // 
         this.Frame_Lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.Frame_Lbl.Location = new System.Drawing.Point(17, 66);
         this.Frame_Lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
         this.Frame_Lbl.Name = "Frame_Lbl";
         this.Frame_Lbl.Size = new System.Drawing.Size(457, 45);
         this.Frame_Lbl.TabIndex = 1;
         this.Frame_Lbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         this.Frame_Lbl.UseMnemonic = false;
         // 
         // SaveMel_Btn
         // 
         this.SaveMel_Btn.Location = new System.Drawing.Point(301, 13);
         this.SaveMel_Btn.Margin = new System.Windows.Forms.Padding(4);
         this.SaveMel_Btn.Name = "SaveMel_Btn";
         this.SaveMel_Btn.Size = new System.Drawing.Size(173, 36);
         this.SaveMel_Btn.TabIndex = 14;
         this.SaveMel_Btn.Text = "Save Mel for Ball Man";
         this.SaveMel_Btn.UseVisualStyleBackColor = true;
         this.SaveMel_Btn.Click += new System.EventHandler(this.SaveMel_Btn_Click);
         // 
         // IncInferred_CB
         // 
         this.IncInferred_CB.AutoSize = true;
         this.IncInferred_CB.Checked = true;
         this.IncInferred_CB.CheckState = System.Windows.Forms.CheckState.Checked;
         this.IncInferred_CB.Location = new System.Drawing.Point(496, 68);
         this.IncInferred_CB.Margin = new System.Windows.Forms.Padding(4);
         this.IncInferred_CB.Name = "IncInferred_CB";
         this.IncInferred_CB.Size = new System.Drawing.Size(156, 20);
         this.IncInferred_CB.TabIndex = 30;
         this.IncInferred_CB.Text = "Include Inferred Joints";
         this.IncInferred_CB.UseVisualStyleBackColor = true;
         // 
         // IncShaders_CB
         // 
         this.IncShaders_CB.AutoSize = true;
         this.IncShaders_CB.Checked = true;
         this.IncShaders_CB.CheckState = System.Windows.Forms.CheckState.Checked;
         this.IncShaders_CB.Location = new System.Drawing.Point(686, 68);
         this.IncShaders_CB.Margin = new System.Windows.Forms.Padding(4);
         this.IncShaders_CB.Name = "IncShaders_CB";
         this.IncShaders_CB.Size = new System.Drawing.Size(124, 20);
         this.IncShaders_CB.TabIndex = 32;
         this.IncShaders_CB.Text = "Include Shaders";
         this.IncShaders_CB.UseVisualStyleBackColor = true;
         // 
         // JointList_CB
         // 
         this.JointList_CB.Location = new System.Drawing.Point(22, 123);
         this.JointList_CB.Margin = new System.Windows.Forms.Padding(4);
         this.JointList_CB.Name = "JointList_CB";
         this.JointList_CB.Size = new System.Drawing.Size(221, 24);
         this.JointList_CB.TabIndex = 50;
         this.JointList_CB.SelectedIndexChanged += new System.EventHandler(this.JointList_CB_SelectedIndexChanged);
         // 
         // Unsmooth_Cht
         // 
         chartArea1.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
         chartArea1.AxisX.ScaleBreakStyle.BreakLineStyle = System.Windows.Forms.DataVisualization.Charting.BreakLineStyle.Straight;
         chartArea1.AxisX2.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
         chartArea1.AxisY2.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
         chartArea1.Name = "ChartArea1";
         this.Unsmooth_Cht.ChartAreas.Add(chartArea1);
         legend1.Name = "Legend1";
         this.Unsmooth_Cht.Legends.Add(legend1);
         this.Unsmooth_Cht.Location = new System.Drawing.Point(22, 158);
         this.Unsmooth_Cht.Name = "Unsmooth_Cht";
         series1.ChartArea = "ChartArea1";
         series1.Legend = "Legend1";
         series1.Name = "Series1";
         this.Unsmooth_Cht.Series.Add(series1);
         this.Unsmooth_Cht.Size = new System.Drawing.Size(1115, 322);
         this.Unsmooth_Cht.TabIndex = 100;
         this.Unsmooth_Cht.Text = "Unsmoothed";
         // 
         // Smooth_Cht
         // 
         chartArea2.AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
         chartArea2.AxisX.ScaleBreakStyle.BreakLineStyle = System.Windows.Forms.DataVisualization.Charting.BreakLineStyle.Straight;
         chartArea2.AxisX2.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
         chartArea2.AxisY2.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
         chartArea2.Name = "ChartArea1";
         this.Smooth_Cht.ChartAreas.Add(chartArea2);
         legend2.Name = "Legend1";
         this.Smooth_Cht.Legends.Add(legend2);
         this.Smooth_Cht.Location = new System.Drawing.Point(22, 487);
         this.Smooth_Cht.Name = "Smooth_Cht";
         series2.ChartArea = "ChartArea1";
         series2.Legend = "Legend1";
         series2.Name = "Series1";
         this.Smooth_Cht.Series.Add(series2);
         this.Smooth_Cht.Size = new System.Drawing.Size(1115, 322);
         this.Smooth_Cht.TabIndex = 102;
         this.Smooth_Cht.Text = "Unsmoothed";
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(495, 100);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(72, 16);
         this.label1.TabIndex = 11;
         this.label1.Text = "Smoothing";
         this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         this.label1.UseMnemonic = false;
         // 
         // Smooth_Parm_EB
         // 
         this.Smooth_Parm_EB.Location = new System.Drawing.Point(495, 123);
         this.Smooth_Parm_EB.Name = "Smooth_Parm_EB";
         this.Smooth_Parm_EB.Size = new System.Drawing.Size(79, 22);
         this.Smooth_Parm_EB.TabIndex = 34;
         this.Smooth_Parm_EB.Text = "0.1";
         // 
         // Correction_Parm_EB
         // 
         this.Correction_Parm_EB.Location = new System.Drawing.Point(597, 123);
         this.Correction_Parm_EB.Name = "Correction_Parm_EB";
         this.Correction_Parm_EB.Size = new System.Drawing.Size(79, 22);
         this.Correction_Parm_EB.TabIndex = 36;
         this.Correction_Parm_EB.Text = "0.1";
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(597, 100);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(69, 16);
         this.label2.TabIndex = 13;
         this.label2.Text = "Correction";
         this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         this.label2.UseMnemonic = false;
         // 
         // Pred_Parm_EB
         // 
         this.Pred_Parm_EB.Location = new System.Drawing.Point(707, 123);
         this.Pred_Parm_EB.Name = "Pred_Parm_EB";
         this.Pred_Parm_EB.Size = new System.Drawing.Size(79, 22);
         this.Pred_Parm_EB.TabIndex = 38;
         this.Pred_Parm_EB.Text = "0.1";
         // 
         // label3
         // 
         this.label3.AutoSize = true;
         this.label3.Location = new System.Drawing.Point(707, 100);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(68, 16);
         this.label3.TabIndex = 15;
         this.label3.Text = "Prediction";
         this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         this.label3.UseMnemonic = false;
         // 
         // Jitter_Parm_EB
         // 
         this.Jitter_Parm_EB.Location = new System.Drawing.Point(815, 123);
         this.Jitter_Parm_EB.Name = "Jitter_Parm_EB";
         this.Jitter_Parm_EB.Size = new System.Drawing.Size(79, 22);
         this.Jitter_Parm_EB.TabIndex = 40;
         this.Jitter_Parm_EB.Text = ".4";
         // 
         // label4
         // 
         this.label4.AutoSize = true;
         this.label4.Location = new System.Drawing.Point(815, 100);
         this.label4.Name = "label4";
         this.label4.Size = new System.Drawing.Size(82, 16);
         this.label4.TabIndex = 17;
         this.label4.Text = "Jitter Radius";
         this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         this.label4.UseMnemonic = false;
         // 
         // MaxDev_Parm_EB
         // 
         this.MaxDev_Parm_EB.Location = new System.Drawing.Point(920, 123);
         this.MaxDev_Parm_EB.Name = "MaxDev_Parm_EB";
         this.MaxDev_Parm_EB.Size = new System.Drawing.Size(79, 22);
         this.MaxDev_Parm_EB.TabIndex = 42;
         this.MaxDev_Parm_EB.Text = ".6";
         // 
         // label5
         // 
         this.label5.AutoSize = true;
         this.label5.Location = new System.Drawing.Point(920, 100);
         this.label5.Name = "label5";
         this.label5.Size = new System.Drawing.Size(90, 16);
         this.label5.TabIndex = 19;
         this.label5.Text = "Max Dev Rad";
         this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         this.label5.UseMnemonic = false;
         // 
         // Smooth_Btn
         // 
         this.Smooth_Btn.Location = new System.Drawing.Point(1018, 109);
         this.Smooth_Btn.Margin = new System.Windows.Forms.Padding(4);
         this.Smooth_Btn.Name = "Smooth_Btn";
         this.Smooth_Btn.Size = new System.Drawing.Size(119, 36);
         this.Smooth_Btn.TabIndex = 50;
         this.Smooth_Btn.Text = "Smooth";
         this.Smooth_Btn.UseVisualStyleBackColor = true;
         this.Smooth_Btn.Click += new System.EventHandler(this.Smooth_Btn_Click);
         // 
         // label6
         // 
         this.label6.AutoSize = true;
         this.label6.Location = new System.Drawing.Point(495, 23);
         this.label6.Name = "label6";
         this.label6.Size = new System.Drawing.Size(171, 16);
         this.label6.TabIndex = 22;
         this.label6.Text = "Save Mel Scripts to Folder: ";
         this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         this.label6.UseMnemonic = false;
         // 
         // MelSavePath_TB
         // 
         this.MelSavePath_TB.Location = new System.Drawing.Point(686, 23);
         this.MelSavePath_TB.MaxLength = 250;
         this.MelSavePath_TB.Name = "MelSavePath_TB";
         this.MelSavePath_TB.Size = new System.Drawing.Size(451, 22);
         this.MelSavePath_TB.TabIndex = 16;
         // 
         // Main_Frm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(1160, 834);
         this.Controls.Add(this.MelSavePath_TB);
         this.Controls.Add(this.label6);
         this.Controls.Add(this.Smooth_Btn);
         this.Controls.Add(this.MaxDev_Parm_EB);
         this.Controls.Add(this.label5);
         this.Controls.Add(this.Jitter_Parm_EB);
         this.Controls.Add(this.label4);
         this.Controls.Add(this.Pred_Parm_EB);
         this.Controls.Add(this.label3);
         this.Controls.Add(this.Correction_Parm_EB);
         this.Controls.Add(this.label2);
         this.Controls.Add(this.Smooth_Parm_EB);
         this.Controls.Add(this.label1);
         this.Controls.Add(this.Smooth_Cht);
         this.Controls.Add(this.Unsmooth_Cht);
         this.Controls.Add(this.JointList_CB);
         this.Controls.Add(this.IncShaders_CB);
         this.Controls.Add(this.IncInferred_CB);
         this.Controls.Add(this.SaveMel_Btn);
         this.Controls.Add(this.Frame_Lbl);
         this.Controls.Add(this.StatusBar);
         this.Controls.Add(this.Stop_Btn);
         this.Controls.Add(this.Start_Btn);
         this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.Margin = new System.Windows.Forms.Padding(4);
         this.Name = "Main_Frm";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Kinect Body Capture";
         ((System.ComponentModel.ISupportInitialize)(this.Unsmooth_Cht)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.Smooth_Cht)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Button Start_Btn;
      private System.Windows.Forms.Button Stop_Btn;
      private System.Windows.Forms.StatusStrip StatusBar;
      private System.Windows.Forms.Label Frame_Lbl;
      private System.Windows.Forms.Button SaveMel_Btn;
      private System.Windows.Forms.CheckBox IncInferred_CB;
      private System.Windows.Forms.CheckBox IncShaders_CB;
      private System.Windows.Forms.ComboBox JointList_CB;
      private System.Windows.Forms.DataVisualization.Charting.Chart Unsmooth_Cht;
      private System.Windows.Forms.DataVisualization.Charting.Chart Smooth_Cht;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.TextBox Smooth_Parm_EB;
      private System.Windows.Forms.TextBox Correction_Parm_EB;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.TextBox Pred_Parm_EB;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.TextBox Jitter_Parm_EB;
      private System.Windows.Forms.Label label4;
      private System.Windows.Forms.TextBox MaxDev_Parm_EB;
      private System.Windows.Forms.Label label5;
      private System.Windows.Forms.Button Smooth_Btn;
      private System.Windows.Forms.Label label6;
      private System.Windows.Forms.TextBox MelSavePath_TB;
   }
}

