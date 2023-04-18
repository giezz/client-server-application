using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client_gui
{
    public partial class Form1 : Form
    {
        private Socket _socket;
        public Form1()
        {
            InitializeComponent();
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipAddress = Dns.Resolve("localhost").AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 8086);
            
            _socket.Connect(ipEndPoint);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string data = "";
            data += numericUpDown1.Text;
            data += "\n";
            data += numericUpDown2.Text;
            data += "\n";
            data += numericUpDown3.Text;
            data += "\n";
            data += numericUpDown4.Text;
            data += "\n";
            data += numericUpDown5.Text;
            data += "\n";
            data += numericUpDown6.Text;
            data += "\n";

            byte[] requestBuffer = Encoding.ASCII.GetBytes(data);
            _socket.Send(requestBuffer);
            byte[] responseBuffer = new byte[32];
            _socket.Receive(responseBuffer);
            textBox1.Text = Encoding.ASCII.GetString(responseBuffer);
        }
    }
}