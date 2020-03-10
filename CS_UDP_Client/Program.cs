using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace CS_UDP_Client
{
    class Program
    {
        private static string remoteHost = "127.0.0.1";
        private static int remotePort;
        private static int ownPort;
        static void Main(string[] args)
        {
            Console.WriteLine("Remote host:"+ remoteHost);
            //remoteHost = Console.ReadLine();
            Console.WriteLine("Remote port:");
            remotePort = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Own port:");
            ownPort = Int32.Parse(Console.ReadLine());

            Thread thread = new Thread(Receive);
            thread.IsBackground = true;
            thread.Start();

            StartSendLoop();
        }

        private static void StartSendLoop()
        {
            UdpClient udpClient = new UdpClient();
            while (true)
            {
                //Console.WriteLine("Write a message n press Enter:");
                DateTime dmessage = DateTime.Now;
                string message = dmessage.ToString();
                Thread.Sleep(1000);
                byte[] messageArray =
                    Encoding.UTF8.GetBytes(message);
                udpClient.Send(messageArray, messageArray.Length, remoteHost, remotePort);
            }
        }

        private static void Receive()
        {
            UdpClient udpClient = new UdpClient(ownPort);
            IPEndPoint remoteEP = null;
            while (true)
            {
                byte[] resultArray =
                    udpClient.Receive(ref remoteEP);
                Console.WriteLine($"Data from {remoteEP.Address}:{remoteEP.Port}:");
                Console.WriteLine(Encoding.UTF8.GetString(resultArray));
            }
        }
    }
}
