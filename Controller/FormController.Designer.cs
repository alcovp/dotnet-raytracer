namespace Controller
{
    partial class FormController
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up_v any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.xResolution_textBox = new System.Windows.Forms.TextBox();
            this.yResolution_textBox = new System.Windows.Forms.TextBox();
            this.Solve_button = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.realTimeBtn = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.xResolutionFast_textBox = new System.Windows.Forms.TextBox();
            this.yResolutionFast_textBox = new System.Windows.Forms.TextBox();
            this.recursion_textBox = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBoxMainResolution = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBoxRealtimeResolution = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxPixelSize = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBoxSettings = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelCoords = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelBuildTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBoxMainResolution.SuspendLayout();
            this.groupBoxRealtimeResolution.SuspendLayout();
            this.groupBoxSettings.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // xResolution_textBox
            // 
            this.xResolution_textBox.Location = new System.Drawing.Point(42, 19);
            this.xResolution_textBox.Name = "xResolution_textBox";
            this.xResolution_textBox.Size = new System.Drawing.Size(49, 20);
            this.xResolution_textBox.TabIndex = 0;
            this.xResolution_textBox.Text = "320";
            // 
            // yResolution_textBox
            // 
            this.yResolution_textBox.Location = new System.Drawing.Point(120, 19);
            this.yResolution_textBox.Name = "yResolution_textBox";
            this.yResolution_textBox.Size = new System.Drawing.Size(49, 20);
            this.yResolution_textBox.TabIndex = 1;
            this.yResolution_textBox.Text = "240";
            // 
            // Solve_button
            // 
            this.Solve_button.Location = new System.Drawing.Point(0, 232);
            this.Solve_button.Margin = new System.Windows.Forms.Padding(3, 3, 3, 25);
            this.Solve_button.Name = "Solve_button";
            this.Solve_button.Size = new System.Drawing.Size(97, 35);
            this.Solve_button.TabIndex = 2;
            this.Solve_button.Text = "Run";
            this.Solve_button.UseVisualStyleBackColor = true;
            this.Solve_button.Click += new System.EventHandler(this.Solve_button_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "0",
            "1",
            "2"});
            this.comboBox1.Location = new System.Drawing.Point(126, 172);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(49, 21);
            this.comboBox1.TabIndex = 5;
            // 
            // realTimeBtn
            // 
            this.realTimeBtn.BackColor = System.Drawing.Color.Firebrick;
            this.realTimeBtn.Location = new System.Drawing.Point(98, 232);
            this.realTimeBtn.Name = "realTimeBtn";
            this.realTimeBtn.Size = new System.Drawing.Size(97, 35);
            this.realTimeBtn.TabIndex = 6;
            this.realTimeBtn.Text = "Real Time Run";
            this.realTimeBtn.UseVisualStyleBackColor = false;
            this.realTimeBtn.Click += new System.EventHandler(this.realTimeBtn_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 40;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // xResolutionFast_textBox
            // 
            this.xResolutionFast_textBox.Location = new System.Drawing.Point(42, 20);
            this.xResolutionFast_textBox.Name = "xResolutionFast_textBox";
            this.xResolutionFast_textBox.Size = new System.Drawing.Size(49, 20);
            this.xResolutionFast_textBox.TabIndex = 8;
            this.xResolutionFast_textBox.Text = "32";
            // 
            // yResolutionFast_textBox
            // 
            this.yResolutionFast_textBox.Location = new System.Drawing.Point(120, 20);
            this.yResolutionFast_textBox.Name = "yResolutionFast_textBox";
            this.yResolutionFast_textBox.Size = new System.Drawing.Size(49, 20);
            this.yResolutionFast_textBox.TabIndex = 9;
            this.yResolutionFast_textBox.Text = "24";
            // 
            // recursion_textBox
            // 
            this.recursion_textBox.Location = new System.Drawing.Point(126, 199);
            this.recursion_textBox.Name = "recursion_textBox";
            this.recursion_textBox.Size = new System.Drawing.Size(49, 20);
            this.recursion_textBox.TabIndex = 10;
            this.recursion_textBox.Text = "3";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(195, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(320, 240);
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // groupBoxMainResolution
            // 
            this.groupBoxMainResolution.Controls.Add(this.label6);
            this.groupBoxMainResolution.Controls.Add(this.label3);
            this.groupBoxMainResolution.Controls.Add(this.xResolution_textBox);
            this.groupBoxMainResolution.Controls.Add(this.yResolution_textBox);
            this.groupBoxMainResolution.Location = new System.Drawing.Point(6, 19);
            this.groupBoxMainResolution.Name = "groupBoxMainResolution";
            this.groupBoxMainResolution.Size = new System.Drawing.Size(183, 54);
            this.groupBoxMainResolution.TabIndex = 13;
            this.groupBoxMainResolution.TabStop = false;
            this.groupBoxMainResolution.Text = "Single run";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(97, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Y:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "X:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // groupBoxRealtimeResolution
            // 
            this.groupBoxRealtimeResolution.Controls.Add(this.label7);
            this.groupBoxRealtimeResolution.Controls.Add(this.textBoxPixelSize);
            this.groupBoxRealtimeResolution.Controls.Add(this.label5);
            this.groupBoxRealtimeResolution.Controls.Add(this.label4);
            this.groupBoxRealtimeResolution.Controls.Add(this.xResolutionFast_textBox);
            this.groupBoxRealtimeResolution.Controls.Add(this.yResolutionFast_textBox);
            this.groupBoxRealtimeResolution.Location = new System.Drawing.Point(6, 79);
            this.groupBoxRealtimeResolution.Name = "groupBoxRealtimeResolution";
            this.groupBoxRealtimeResolution.Size = new System.Drawing.Size(183, 87);
            this.groupBoxRealtimeResolution.TabIndex = 14;
            this.groupBoxRealtimeResolution.TabStop = false;
            this.groupBoxRealtimeResolution.Text = "Realtime run";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Pixel size:";
            // 
            // textBoxPixelSize
            // 
            this.textBoxPixelSize.Location = new System.Drawing.Point(120, 46);
            this.textBoxPixelSize.Name = "textBoxPixelSize";
            this.textBoxPixelSize.Size = new System.Drawing.Size(49, 20);
            this.textBoxPixelSize.TabIndex = 11;
            this.textBoxPixelSize.Text = "10";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(97, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Y:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "X:";
            // 
            // groupBoxSettings
            // 
            this.groupBoxSettings.Controls.Add(this.label9);
            this.groupBoxSettings.Controls.Add(this.label8);
            this.groupBoxSettings.Controls.Add(this.groupBoxMainResolution);
            this.groupBoxSettings.Controls.Add(this.recursion_textBox);
            this.groupBoxSettings.Controls.Add(this.groupBoxRealtimeResolution);
            this.groupBoxSettings.Controls.Add(this.comboBox1);
            this.groupBoxSettings.Location = new System.Drawing.Point(0, 0);
            this.groupBoxSettings.Name = "groupBoxSettings";
            this.groupBoxSettings.Size = new System.Drawing.Size(195, 226);
            this.groupBoxSettings.TabIndex = 15;
            this.groupBoxSettings.TabStop = false;
            this.groupBoxSettings.Text = "Settings";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 202);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "Recursion depth:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 175);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Antialiasing:";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelCoords,
            this.toolStripStatusLabelBuildTime,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 270);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(515, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 16;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelCoords
            // 
            this.toolStripStatusLabelCoords.Name = "toolStripStatusLabelCoords";
            this.toolStripStatusLabelCoords.Size = new System.Drawing.Size(71, 17);
            this.toolStripStatusLabelCoords.Text = "Coordinates";
            // 
            // toolStripStatusLabelBuildTime
            // 
            this.toolStripStatusLabelBuildTime.Name = "toolStripStatusLabelBuildTime";
            this.toolStripStatusLabelBuildTime.Size = new System.Drawing.Size(76, 17);
            this.toolStripStatusLabelBuildTime.Text = "Build time     ";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(300, 16);
            this.toolStripProgressBar1.Step = 1;
            this.toolStripProgressBar1.Visible = false;
            // 
            // FormController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(515, 292);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBoxSettings);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.realTimeBtn);
            this.Controls.Add(this.Solve_button);
            this.KeyPreview = true;
            this.Name = "FormController";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Controller";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormController_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBoxMainResolution.ResumeLayout(false);
            this.groupBoxMainResolution.PerformLayout();
            this.groupBoxRealtimeResolution.ResumeLayout(false);
            this.groupBoxRealtimeResolution.PerformLayout();
            this.groupBoxSettings.ResumeLayout(false);
            this.groupBoxSettings.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox xResolution_textBox;
        private System.Windows.Forms.TextBox yResolution_textBox;
        private System.Windows.Forms.Button Solve_button;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button realTimeBtn;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox xResolutionFast_textBox;
        private System.Windows.Forms.TextBox yResolutionFast_textBox;
        private System.Windows.Forms.TextBox recursion_textBox;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBoxMainResolution;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBoxRealtimeResolution;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBoxSettings;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxPixelSize;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelCoords;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelBuildTime;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
    }
}

