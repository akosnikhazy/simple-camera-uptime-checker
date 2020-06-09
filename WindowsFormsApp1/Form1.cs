using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace WindowsFormsApp1
{


    public partial class Form1 : Form
    {



        Boolean itIsDown = false;

        DateTime blackoutStartTime;
        DateTime blackoutStopTime;



        protected void Page_Load()
        {



            try
            {
                WebClient client = new WebClient();
                string result = client.DownloadString("YOUR URL TO CHECK");

                if (itIsDown)
                {
                    itIsDown = false;
                    blackoutStopTime = DateTime.Now;

                    textBox1.AppendText("\r\n");
                    textBox1.AppendText("The cam is active: " + blackoutStopTime + "\r\n");

                    TimeSpan time = TimeSpan.FromSeconds(blackoutStopTime.Subtract(blackoutStartTime).TotalSeconds);
                    string blackoutTime = time.ToString(@"hh\:mm\:ss");

                    textBox1.AppendText("Downtime: " + blackoutTime);
                    textBox1.AppendText("\r\n");
                    textBox1.AppendText("-----------------------------" + "\r\n");

                    File.WriteAllText("log-" + DateTime.Now.ToString("yyyyMMdd") + ".txt", textBox1.Text);


                }
            }
            catch (WebException ex)
            {
                if (!itIsDown)
                {
                    blackoutStartTime = DateTime.Now;
                    itIsDown = true;
                    textBox1.AppendText("The cam is inactive from " + blackoutStartTime);

                    File.WriteAllText("log-" + DateTime.Now.ToString("yyyyMMdd") + ".txt", textBox1.Text);


                }


            }

        }

        public Form1()
        {
            InitializeComponent();


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Page_Load();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start("notepad.exe", "log-" + DateTime.Now.ToString("yyyyMMdd") + ".txt");
        }
    }
}