using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace server
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipAddress = Dns.Resolve("localhost").AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(ipAddress, 8086);
            listener.Bind(endPoint);
            listener.Listen(10);
            Console.WriteLine("Waiting for connection...");
            Socket socket = listener.Accept();
            byte[] receiveBuffer = new byte[1024];
            int sum = 0;
            for (int i = 0; i < 3; i++)
            {
                socket.Receive(receiveBuffer);
                string data = Encoding.ASCII.GetString(receiveBuffer);
                Console.WriteLine($"{i + 1} number is {data}");
                sum += Convert.ToInt32(data);
            }

            byte[] sendBuffer = Encoding.ASCII.GetBytes(sum.ToString());
            socket.Send(sendBuffer);
            Console.WriteLine($"Sum of numbers: {sum} send");
            socket.Close();
        }
    }
}