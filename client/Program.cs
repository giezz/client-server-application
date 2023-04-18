using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace client
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipAddress = Dns.Resolve("localhost").AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 8086);
            
            socket.Connect(ipEndPoint);
            Console.WriteLine("Connection established");
            Console.WriteLine("Enter 3 numbers");

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"Enter {i + 1} number");
                string str = Console.ReadLine();
                str += "\n";
                byte[] buffer = Encoding.ASCII.GetBytes(str);
                socket.Send(buffer);
                Console.WriteLine($"Number {i + 1} transmitted");
            }

            Console.WriteLine("All numbers transmitted");
            byte[] receivedBuffer = new byte[32];
            socket.Receive(receivedBuffer);
            Console.WriteLine($"Serverside calculated sum of numbers: {Encoding.ASCII.GetString(receivedBuffer)}");
            socket.Close();
        }
    }
}
