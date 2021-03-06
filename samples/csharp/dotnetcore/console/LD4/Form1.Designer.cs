﻿namespace LieDetector3
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
            this.button1 = new System.Windows.Forms.Button();
            this.cmb_CameraDevice = new System.Windows.Forms.ComboBox();
            this.cmb_CameraResolution = new System.Windows.Forms.ComboBox();
            this.picCamera = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.LyingScoreBox = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.LyingScoreBox2 = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picCamera)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(205, 701);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(179, 61);
            this.button1.TabIndex = 0;
            this.button1.Text = "开始录制";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // cmb_CameraDevice
            // 
            this.cmb_CameraDevice.FormattingEnabled = true;
            this.cmb_CameraDevice.Location = new System.Drawing.Point(205, 46);
            this.cmb_CameraDevice.Name = "cmb_CameraDevice";
            this.cmb_CameraDevice.Size = new System.Drawing.Size(405, 21);
            this.cmb_CameraDevice.TabIndex = 1;
            this.cmb_CameraDevice.SelectedIndexChanged += new System.EventHandler(this.Cmb_CameraDevice_SelectedIndexChanged);
            // 
            // cmb_CameraResolution
            // 
            this.cmb_CameraResolution.FormattingEnabled = true;
            this.cmb_CameraResolution.Location = new System.Drawing.Point(205, 71);
            this.cmb_CameraResolution.Name = "cmb_CameraResolution";
            this.cmb_CameraResolution.Size = new System.Drawing.Size(405, 21);
            this.cmb_CameraResolution.TabIndex = 2;
            this.cmb_CameraResolution.SelectedIndexChanged += new System.EventHandler(this.Cmb_CameraResolution_SelectedIndexChanged);
            // 
            // picCamera
            // 
            this.picCamera.Location = new System.Drawing.Point(148, 116);
            this.picCamera.Name = "picCamera";
            this.picCamera.Size = new System.Drawing.Size(563, 536);
            this.picCamera.TabIndex = 3;
            this.picCamera.TabStop = false;
            this.picCamera.Click += new System.EventHandler(this.PicCamera_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(423, 701);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(187, 59);
            this.button2.TabIndex = 4;
            this.button2.Text = "停止录制";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1364, 251);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(179, 59);
            this.button3.TabIndex = 5;
            this.button3.Text = "开始语音检测";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // LyingScoreBox
            // 
            this.LyingScoreBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LyingScoreBox.Location = new System.Drawing.Point(1035, 46);
            this.LyingScoreBox.Multiline = true;
            this.LyingScoreBox.Name = "LyingScoreBox";
            this.LyingScoreBox.Size = new System.Drawing.Size(229, 54);
            this.LyingScoreBox.TabIndex = 9;
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Location = new System.Drawing.Point(859, 46);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(155, 54);
            this.button5.TabIndex = 8;
            this.button5.Text = "Lying Score";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(859, 106);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(405, 305);
            this.textBox1.TabIndex = 7;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pictureBox1.Location = new System.Drawing.Point(859, 513);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(404, 305);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // LyingScoreBox2
            // 
            this.LyingScoreBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LyingScoreBox2.Location = new System.Drawing.Point(1034, 453);
            this.LyingScoreBox2.Multiline = true;
            this.LyingScoreBox2.Name = "LyingScoreBox2";
            this.LyingScoreBox2.Size = new System.Drawing.Size(229, 54);
            this.LyingScoreBox2.TabIndex = 12;
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(859, 453);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(155, 54);
            this.button4.TabIndex = 11;
            this.button4.Text = "Lying Score";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(1364, 593);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(179, 59);
            this.button6.TabIndex = 13;
            this.button6.Text = "开始视频检测";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.Button6_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1781, 1011);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.LyingScoreBox2);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.LyingScoreBox);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.picCamera);
            this.Controls.Add(this.cmb_CameraResolution);
            this.Controls.Add(this.cmb_CameraDevice);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.picCamera)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cmb_CameraDevice;
        private System.Windows.Forms.ComboBox cmb_CameraResolution;
        private System.Windows.Forms.PictureBox picCamera;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox LyingScoreBox;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox LyingScoreBox2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button6;
    }
}

