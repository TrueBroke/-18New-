using System;
using System.Net;
using System.Net.Sockets;
using System.Text;



namespace урок18New
{

    class Program
    {
        static void Main(string[] args)
        {
            // Сокеты (socket) и клиент-серверное взаимодействие по протоколам TCP и UDP 
            // TCP - устанавливает четкое соединения между клиентом и сервером (канал). и обеспечивает гарантию что будет доставлен ответ и запрос 100%. минус в том что работает медленне и даёт большую нагрузку.
            // UDP - не очень четкое, но подходит для большого количества оповещений.
            // TCP(пример) - подойти к кому то лично сказать. UDP - крикнуть сразу всем.
            // Работа с сайтом - TCP. UDP например для стримингого вещания
            // Адрес приложения в сети, нужно 2 параметра: Айпи адрес и порт.

            #region TCP
            
            const string ip = "127.0.0.1"; // создание айпи.
            const int port = 8080; // создание порта.

            // endpoint - Точка подключения

            var tcpEndPoint =  new IPEndPoint(IPAddress.Parse(ip), port); // создали endpoint.

            // socket - дверка в которую можно заходить, через которую устанавливается соединение

            var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); // набор параметров сокета для Tcp протокола. Создали сокет

            // надо перевести наш сокет в режим ожидания. Надо обозначит сокету что необходимо слушать конкертно этот порт.

            tcpSocket.Bind(tcpEndPoint); //связали сокет и порт
            tcpSocket.Listen(5); // в скобочках можно создать очередь подключения, когда на один и тот же порт может подключаться несколько клиентов. сделали допустимую очередь из 5 человек. Также мы сокет сделали слушателем

            // если надо несколько сектом типа Tcp, то создаем еще порты и Endпоинты, далее с ними сздаем новые сокеты

            while (true)
            {
                // обработчик на прием сообщений
                var listener = tcpSocket.Accept();
                var buffer = new byte[256]; // та переменная куда данные будут переходить. Хранилище данных
                var size = 0; // переменная куда будет сохраняться реальное колчество байтов
                var data = new StringBuilder();


                do
                {
                    size = listener.Receive(buffer);
                    data.Append(Encoding.UTF8.GetString(buffer, 0, size)); // перекодировка в строку
                }
                while (listener.Available > 0);

                Console.WriteLine(data);

                listener.Send(Encoding.UTF8.GetBytes("Успех"));

                listener.Shutdown(SocketShutdown.Both); // делаем двухсторонние закрытие. у клиента и у сервера.
                listener.Close();

                Console.ReadLine();
            }

            #endregion


            #region UDP
            /*
            const string ip = "127.0.0.1"; // создание айпи.
            const int port = 8081; // создание порта.

            var udpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port); // создали подключение.

            var udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp); // меняем параметр подключения. создали сокет
            udpSocket.Bind(udpEndPoint); // связываем с точкой.

            while (true)
            {
                var buffer = new byte[256];
                var size = 0;
                var data = new StringBuilder();

                EndPoint senderEndPoint = new IPEndPoint(IPAddress.Any, 0); //создали экземпляр адресса, в которых будут записаны данные (адрес клиента).

                do
                {
                    size = udpSocket.ReceiveFrom(buffer, ref senderEndPoint);
                    data.Append(Encoding.UTF8.GetString(buffer));
                }
                while (udpSocket.Available > 0);

                udpSocket.SendTo(Encoding.UTF8.GetBytes("Сообщение получено"), senderEndPoint);



                Console.WriteLine(data);
            
            }
            */
            

























#endregion
        }









    }
}