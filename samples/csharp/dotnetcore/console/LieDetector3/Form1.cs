using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DarrenLee.Media;
using NAudio.Wave;

namespace LieDetector3
{
    public partial class Form1 : Form
    {
        //string ROOT = Application.StartupPath + "\\";
        static string ROOT = @"C:\\Hackthon\\";

        int count = 0;
        Camera myCamera = new Camera();
        [System.Runtime.InteropServices.DllImport("winmm.dll")]
        private static extern long mciSendString(string command, StringBuilder retstring, int ReturnLenth, IntPtr callback);


        public Form1()
        {
            InitializeComponent();
            mciSendString("Open new Type waveaudio alias recsound", null, 0, IntPtr.Zero);
            button1.Click += new EventHandler(this.Button1_Click);
            GetInfo();
            myCamera.OnFrameArrived += MyCamera_OnFrameArrived;
        }

        private void GetInfo()
        {
            var cameraDevices = myCamera.GetCameraSources();
            var cameraResolutions = myCamera.GetSupportedResolutions();
            foreach (var d in cameraDevices)
            {
                cmb_CameraDevice.Items.Add(d);
            }
            foreach (var r in cameraResolutions)
            {
                cmb_CameraResolution.Items.Add(r);
            }
            cmb_CameraDevice.SelectedIndex = 0;
            cmb_CameraResolution.SelectedIndex = 0;
        }

        private void MyCamera_OnFrameArrived(object source, FrameArrivedEventArgs e)
        {
            Image img = e.GetFrame();
            picCamera.Image = img;
        }

        private void Cmb_CameraDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            myCamera.ChangeCamera(cmb_CameraDevice.SelectedIndex);
        }

        private void Cmb_CameraResolution_SelectedIndexChanged(object sender, EventArgs e)
        {
            myCamera.Start(cmb_CameraResolution.SelectedIndex);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            myCamera.Stop();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            mciSendString("record recsound", null, 0, IntPtr.Zero);
            button2.Click += new EventHandler(this.Button2_Click);
            string filePath = Application.StartupPath + @"\" + "Image_" + count.ToString();
            for (int i = 0; i < 10; i++)
            {
                filePath = ROOT + @"\Image_" + i.ToString();
                myCamera.Capture(filePath);
                wait(3000);
            }
        }
        private void Button2_Click(object sender, EventArgs e)
        {

            mciSendString("save recsound " + ROOT + @"record.wav", null, 0, IntPtr.Zero);
            mciSendString("close recsound ", null, 0, IntPtr.Zero);
            var input = File.ReadAllBytes(ROOT + @"record.wav");
            var output = ConvertWavTo8000Hz16BitMonoWav(input);
            File.WriteAllBytes(ROOT + "record_16.wav", output);
            button3.Click += new EventHandler(this.Button3_Click);

        }
        private async void Button3_Click(object sender, EventArgs e)
        {
            var pair = await SpeechRecognization.ContinuousRecognitionWithFileAsync();
            var pair_cv = await ImageUnderstanding.ImageUnderstandingAsync(ROOT);
            textBox1.Text = pair.Key;
            pictureBox1.Image = Image.FromFile(ROOT + @"\Image_" + pair_cv.Item1.ToString()+".jpg");
            double score = pair.Value;
            double score2 = pair_cv.Item2;
            LyingScoreBox.Text = Math.Round(score, 2) + "";
            if (score < 40)
            {
                LyingScoreBox.ForeColor = Color.Green;

                //MessageBox.Show("No lies\nlying score: " + score);
            }
            else if (score < 75)
            {
                //MessageBox.Show("You may be lying!");
                LyingScoreBox.ForeColor = Color.Yellow;
            }
            else
            {
                //MessageBox.Show("You are lying!!!");
                LyingScoreBox.ForeColor = Color.Red;
            }
            //MessageBox.Show(pair.Key+":"+pair.Value);

            if (score2 < 40)
            {
                LyingScoreBox.ForeColor = Color.Green;

                //MessageBox.Show("No lies\nlying score: " + score);
            }
            else if (score2 < 75)
            {
                //MessageBox.Show("You may be lying!");
                LyingScoreBox.ForeColor = Color.Yellow;
            }
            else
            {
                //MessageBox.Show("You are lying!!!");
                LyingScoreBox.ForeColor = Color.Red;
            }
        }
        public byte[] ConvertWavTo8000Hz16BitMonoWav(byte[] inArray)
        {
            using (var mem = new MemoryStream(inArray))
            using (var reader = new WaveFileReader(mem))
            using (var converter = WaveFormatConversionStream.CreatePcmStream(reader))
            using (var upsampler = new WaveFormatConversionStream(new WaveFormat(8000, 16, 1), converter))
            {
                // todo: without saving to file using MemoryStream or similar
                WaveFileWriter.CreateWaveFile(ROOT + "record_temp.wav", upsampler);
                return File.ReadAllBytes(ROOT + "record_temp.wav");
            }
        }
        public void wait(int milliseconds)
        {
            System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0) return;
            //Console.WriteLine("start wait timer");
            timer1.Interval = milliseconds;
            timer1.Enabled = true;
            timer1.Start();
            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                timer1.Stop();
                //Console.WriteLine("stop wait timer");
            };
            while (timer1.Enabled)
            {
                Application.DoEvents();
            }
        }

        private void PicCamera_Click(object sender, EventArgs e)
        {

        }
    }
}
