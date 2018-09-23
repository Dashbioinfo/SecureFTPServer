using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using RSAExample;
namespace GUISimpleTCPServer
{
    public partial class Form1 : Form
    {
        Server server = null;
        Thread th;
        delegate void SetTextCallback(string text);

        public Form1()
        {
            InitializeComponent();
        }
        public void show(string msg)
        {
            if (listBox1.InvokeRequired)
            {

                SetTextCallback d = new SetTextCallback(show);
                this.Invoke(d, new object[] { msg });
            }
            else
            {
                this.listBox1.Items.Add(msg);
            }

        }


        private void Connect_Click(object sender, EventArgs e)
        {
            try
            {
                backgroundWorker1.RunWorkerAsync();
                server = new Server(ipAddress.Text, port.Text, this);
                th = new Thread(new ThreadStart(server.run));
                th.Start();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.UseShellExecute = true;
            Process.Start(Directory.GetCurrentDirectory());
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void port_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
