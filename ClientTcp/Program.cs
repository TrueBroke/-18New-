using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClientTcp
{

    class Program
    {

        static void Main(string[] args)
        {
            #region TCP
            
            // создаём клиента
            const string ip = "127.0.0.1"; // создание айпи.
            const int port = 8080; // создание порта.

            var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port); // создали endpoint.

            var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            Console.WriteLine("Введите сообщение: ");
            var message = Console.ReadLine(); // Вводим сообщение

            var data = Encoding.UTF8.GetBytes(message); //кодируем наше сообщение

            tcpSocket.Connect(tcpEndPoint);
            tcpSocket.Send(data);

            var buffer = new byte[256]; 
            var size = 0; 
            var answer = new StringBuilder(); // ответ сервера

            do
            {
                size = tcpSocket.Receive(buffer); // получили сообщение
                answer.Append(Encoding.UTF8.GetString(buffer, 0, size)); // перекодировка в строку
            }
            while (tcpSocket.Available > 0);

            Console.WriteLine(answer.ToString());
            
            tcpSocket.Shutdown(SocketShutdown.Both);
            tcpSocket.Close();

            Console.ReadLine();
            
            #endregion

            #region UDP
            //udp
            /*
            const string ip = "127.0.0.1"; // создание айпи.
            const int port = 8082; // создание порта.

            var udpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port); // создали endpoint.

            var udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp); // настроили сокет юдп
            udpSocket.Bind(udpEndPoint); // связываем с точкой.

            while (true)
            {
                Console.WriteLine("Введите сообщение: ");
                var message = Console.ReadLine();

                var serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8081); //создали экземпляр адресса, в которых будут записаны данные (адрес клиента).
                udpSocket.SendTo(Encoding.UTF8.GetBytes(message), serverEndPoint);

                var buffer = new byte[256];
                var size = 0;
                var data = new StringBuilder();

                EndPoint senderEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8081); //создали экземпляр адресса, в которых будут записаны данные (адрес клиента).

                do
                {
                    size = udpSocket.ReceiveFrom(buffer, ref senderEndPoint);
                    data.Append(Encoding.UTF8.GetString(buffer));
                }
                while (udpSocket.Available > 0);
                Console.WriteLine(data);
                Console.ReadLine();
            }
            */
            #endregion
        }
    }
}