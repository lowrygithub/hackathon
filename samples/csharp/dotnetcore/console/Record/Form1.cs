using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using NAudio.Wave;

namespace Record
{
    public partial class Form1 : Form
    {
        static string ROOT = @"C:\\Hackthon\\";
        //static string ROOT = @"C:\\Users\\kerli\\Source\\Repos\\cognitive-services-speech-sdk-master\\samples\\csharp\\sharedcontent\\console\\";

        [System.Runtime.InteropServices.DllImport("winmm.dll")]
        private static extern long mciSendString(string command, StringBuilder retstring, int ReturnLenth, IntPtr callback);


        public Form1()
        {
            InitializeComponent();
            mciSendString("Open new Type waveaudio alias recsound", null, 0, IntPtr.Zero);
            button1.Click += new EventHandler(this.Button1_Click);

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            mciSendString("record recsound", null, 0, IntPtr.Zero);
            button2.Click += new EventHandler(this.Button2_Click);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory(ROOT);
            mciSendString("save recsound " + ROOT + "record.wav", null, 0, IntPtr.Zero);
            mciSendString("close recsound ", null, 0, IntPtr.Zero);
            var input = File.ReadAllBytes(ROOT + "record.wav");
            var output = ConvertWavTo8000Hz16BitMonoWav(input);
            File.WriteAllBytes(ROOT + "record_16.wav", output);
            //button3.Click += new EventHandler(this.Button3_Click);

        }

        private async void Button3_Click(object sender, EventArgs e)
        {
            var pair = await SpeechRecognization.ContinuousRecognitionWithFileAsync();
            textBox1.Text = pair.Key;
            double score = pair.Value;
            LyingScoreBox.Text = Math.Round(score, 2) + "";
            if (score < 0.4)
            {
                LyingScoreBox.ForeColor = Color.Green;

                //MessageBox.Show("No lies\nlying score: " + score);
            }
            else if (score < 0.75)
            {
                MessageBox.Show("You may be lying!");
                LyingScoreBox.ForeColor = Color.Yellow;
            }
            else
            {
                MessageBox.Show("You are lying!!!");
                LyingScoreBox.ForeColor = Color.Red;
            }
            //MessageBox.Show(pair.Key+":"+pair.Value);
        }
        public static byte[] ConvertWavTo8000Hz16BitMonoWav(byte[] inArray)
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
        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Button5_Click(object sender, EventArgs e)
        {

        }
    }
}
