using MessageServer.Entity;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace MessageServer
{
    class AsyncMessageServer
    {
        private IPAddress _ipAddress;
        private IPEndPoint _ipEndPoint;
        private Socket _serverSocket;
        private ManualResetEvent _mre;
        private List<Message> _messagesList = new List<Message>();
        static private object locker = new();

        public AsyncMessageServer(string ip, int port)
        {
            if (port <= 0) throw new ArgumentException("Номер порта не коректный");

            _ipAddress = IPAddress.Parse(ip);
            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _ipEndPoint = new IPEndPoint(_ipAddress, port);

            _mre = new ManualResetEvent(false);
        }


        public void StartListening()
        {
            ///Для дебага
            Message message = new Message() { IncomeTime = DateTime.Now , Sender = "Admin", Reccepient ="user", Messages = "Hi user"};
            _messagesList.Add(message);
            _messagesList.Add(new Message() { IncomeTime = DateTime.Now, Sender = "Admin", Reccepient = "user", Messages = "Hi user message 2" });

            ////////////////////////////////



            _serverSocket.Bind(_ipEndPoint);
            _serverSocket.Listen(100);

            Console.WriteLine("--- СЕРВЕР ЗАПУЩЕН ---");

            while (true)
            {
                _mre.Reset();
                _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), _serverSocket);
                _mre.WaitOne();
            }
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            _mre.Set();
            ClientState clientState = new ClientState();

            clientState.WorkSocket = ((Socket)ar.AsyncState).EndAccept(ar);

            clientState.WorkSocket.BeginReceive(clientState.Buffer, 0, ClientState.BufferSize, SocketFlags.Partial, new AsyncCallback(ReeadFinishedCallback), clientState);

        }

        private void ReeadFinishedCallback(IAsyncResult ar)
        {
            ClientState clientState = (ClientState)ar.AsyncState;

            int len = clientState.WorkSocket.EndReceive(ar);

            if (len > 0)
            {
                clientState.Message = Encoding.UTF8.GetString(clientState.Buffer, 0, len);
                //clientState.MyMessage = (Message)MyConverter.ByteArrayToObject(clientState.Buffer);
                //clientState.Message = clientState.MyMessage.Command;

                if (clientState.Message != null)
                {
                    string[] request = clientState.Message.Split("=|=");
                    string command = request[0];
                    string recepient = request[1];


                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"Запрос от клиента: {clientState.WorkSocket.RemoteEndPoint}");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine($"Тело запроса: {command} для пользователя {recepient}");
                    Console.ResetColor();

                    if (command == "Get_message")
                    {
                        string concatingMsg = "";

                        lock (locker)
                        {
                            foreach (Message item in _messagesList)
                            {
                                if (item.Reccepient == recepient)
                                {
                                    concatingMsg += item.ToString() + "=|=";

                                }

                            }
                            _messagesList.RemoveAll(item => item.Reccepient == recepient);
                            SendAnswer(clientState.WorkSocket, concatingMsg);
                        }
                    }
                    else if (command == "sent_message")
                    {
                        string sender = request[2];
                        string message = request[3];
                        
                        lock (locker)
                        {
                            _messagesList.Add(new Message()
                            {
                                Sender = sender,
                                IncomeTime = DateTime.Now,
                                Reccepient = recepient,
                                Messages = message
                            });
                        }
                        //SendAnswer(clientState.WorkSocket, "OK");
                    }


                }
                Console.WriteLine("Количество сообщений на сервере:" + _messagesList.Count);

                ////Ответ переделать нужно.
                //SendAnswer(clientState.WorkSocket, "Привет запрос принят. Ожидайте");
            }


        }

        private void SendAnswer(Socket workSocket, string answerServerToClient)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(answerServerToClient);

            workSocket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(SendAnswerCallBack), workSocket);

        }

        private void SendAnswerCallBack(IAsyncResult ar)
        {
            try
            {
                Socket clientSocket = (Socket)ar.AsyncState;
                clientSocket.EndSend(ar);

                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }

        }
    

}
}
