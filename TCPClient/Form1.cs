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

namespace TCPClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private SimpleTcpClient client;
        private void btnSend_Click(object sender, EventArgs e)
        {

            if (client.IsConnected)
            {
                if (!string.IsNullOrEmpty(txtMessage.Text))
                {
                    client.Send(txtMessage.Text);
                    txtInfo.Text += $"Me : {txtMessage.Text}{Environment.NewLine}";
                    txtMessage.Text = string.Empty;


                }

            }

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {

                client.Connect();
                btnConnect.Enabled = false;
                btnSend.Enabled = true;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message,"Hata Alındı",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            client=new SimpleTcpClient(txtP.Text);
            client.Events.DataReceived += Events_DataReceived;
            client.Events.Connected += Events_Connected;
            client.Events.Disconnected += Events_Disconnected;
        }

        private void Events_Disconnected(object sender, ClientDisconnectedEventArgs e)
        {
            this.Invoke((MethodInvoker) delegate
            {

                txtInfo.Text += $"{e.IpPort} Disconnecting... {Environment.NewLine}";

            });
        }

        private void Events_Connected(object sender, ClientConnectedEventArgs e)
        {
            this.Invoke((MethodInvoker) delegate
            {
                txtInfo.Text += $"{e.IpPort} Connecting... {Environment.NewLine}";
                


            });
        }

        private void Events_DataReceived(object sender, DataReceivedEventArgs e)
        {
            this.Invoke((MethodInvoker) delegate
            {
                txtInfo.Text += $"{e.IpPort} {Encoding.UTF8.GetString(e.Data)}{Environment.NewLine}";






            });
        }
    }
}
