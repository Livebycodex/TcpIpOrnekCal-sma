using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimpleTcp;

namespace TCPServer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private SimpleTcpServer server;

        private void btnStart_Click(object sender, EventArgs e)
        {
            server.Start();
            txtInfo.Text += $"Starting....{Environment.NewLine}";
            btnStart.Enabled = false;
            btnSend.Enabled = true;





        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnSend.Enabled = false;
            server = new SimpleTcpServer(txtP.Text);
            server.Events.DataReceived += Events_DataReceived;
            server.Events.ClientConnected += Events_ClientConnected;
            server.Events.ClientDisconnected += Events_ClientDisconnected;
        }

        private void Events_ClientDisconnected(object sender, ClientDisconnectedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
           {
               txtP.Text += $"{e.IpPort} Disconnecting....{Environment.NewLine}";
               lstClientIP.Items.Remove(e.IpPort);

           });
        }





        private void Events_ClientConnected(object sender, ClientConnectedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
           {

               txtInfo.Text += $"{e.IpPort} Connecting...{Environment.NewLine}";
               lstClientIP.Items.Add(e.IpPort);

           });
        }

        private void Events_DataReceived(object sender, DataReceivedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
           {
               txtInfo.Text += $"{e.IpPort}:{Encoding.UTF8.GetString(e.Data)}{Environment.NewLine}";


           });
        }




        private void btnSend_Click(object sender, EventArgs e)
        {
            if (server.IsListening)
            {
                if (lstClientIP.SelectedItem != null)
                {
                    

                    
                    
                        if (!string.IsNullOrEmpty(txtMessage.Text))
                        {
                            server.Send(lstClientIP.Text, txtMessage.Text);
                            txtInfo.Text += $"Server : {txtMessage.Text}{Environment.NewLine}";
                            txtMessage.Text = string.Empty;


                        }
                        else
                        {
                            MessageBox.Show("Lütfen Mesajınızı yazınız.");
                        }
                    
                 

                }
                else
                {
                    MessageBox.Show("Lütfen Clientın bağlanmasını bekleyiniz ve ip adresinizi giriniz.");
                }
               



            }
        }
    }
}
