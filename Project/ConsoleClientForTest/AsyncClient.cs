using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MessageServer.Entity;

namespace ConsoleClientForTest
{
    internal class AsyncClient
    {
        /// <summary>
        /// для исправление бага с консолью
        /// </summary>
        static private object locker = new();

        static private void ConsoleWriteErorr(string erorrText)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(erorrText);
            Console.ResetColor();
        }

        static public void SendMyMessage(string ip, int port, Message m)
        {
            try
            {
                IPAddress ipAddress = IPAddress.Parse(ip);
                IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, port);

                Socket _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                _socket.BeginConnect(ipEndPoint,
                   new AsyncCallback(ConnectCallback),
                   new ClientState()
                   {
                       Socket = _socket,
                       MyMessage = m,
                       Message = "Send_message"
                   });
            }
            catch (FormatException formatEx)
            {
                ConsoleWriteErorr(formatEx.Message);
            }
            catch (ArgumentException argEx)
            {
                ConsoleWriteErorr(argEx.Message);
            }
            catch (Exception ex)
            {
                ConsoleWriteErorr(ex.Message);
            }
        }




        static public void SendMessage(string ip, int port, string message)
        {
            try
            {
                IPAddress ipAddress = IPAddress.Parse(ip);
                IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, port);

                Socket _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                _socket.BeginConnect(ipEndPoint,
                   new AsyncCallback(ConnectCallback),
                   new ClientState()
                   {
                       Socket = _socket,
                       Message = message
                   });
            }
            catch (FormatException formatEx)
            {
                ConsoleWriteErorr(formatEx.Message);
            }
            catch (ArgumentException argEx)
            {
                ConsoleWriteErorr(argEx.Message);
            }
            catch (Exception ex)
            {
                ConsoleWriteErorr(ex.Message);
            }
        }

        /// <summary>
        /// CALL BACK FUNCTION которая будет передано упрввление при установлении соеденения с удаленнім сервером
        /// </summary>
        /// <param name="ar"></param>
        static private void ConnectCallback(IAsyncResult ar)
        {
            ClientState clientState = (ClientState)ar.AsyncState;

            lock (locker)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Подключение установлено.");
                Console.WriteLine($"{clientState.Socket.RemoteEndPoint}");
                Console.ResetColor();
            }
            ///send data to server
            Send(clientState);


        }


        static private void Send(ClientState state)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(state.Message);
            state.Socket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(SendMessageEndCallBack), state);
        }

        //Callback method при успешной передаче всех данніх на сервер
        static private void SendMessageEndCallBack(IAsyncResult ar)
        {
            ClientState state = (ClientState)ar.AsyncState;
            Socket server = state.Socket;

            lock (locker)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;

                Console.WriteLine("Сообщение было успешно передано на сервер");
                Console.WriteLine($"Обьем переданных данных {server.EndSend(ar)} байт");

                Console.ResetColor();
            }


            ///ПОлучаем ответ от сервера
            ReciveResponce(state);
        }

        /// <summary>
        /// Получает ответ от сервера
        /// </summary>
        static private void ReciveResponce(ClientState state)
        {
            state.ByteMessageResponce = new byte[1024];
            state.Socket.BeginReceive(state.ByteMessageResponce, 0, state.ByteMessageResponce.Length, SocketFlags.None, new AsyncCallback(ReciveResponceCallBack), state);
        }

        /// <summary>
        /// Колбек функция управление которой будет передано при получении ответа от сервера
        /// </summary>
        /// <param name="ar"></param>
        /// <exception cref="NotImplementedException"></exception>
        static private void ReciveResponceCallBack(IAsyncResult ar)
        {
            ClientState state = (ClientState)ar.AsyncState;
            Socket socket = state.Socket;


            int lenUploadetByte = socket.EndReceive(ar);

            string responceFromServer = Encoding.UTF8.GetString(state.ByteMessageResponce, 0, lenUploadetByte);

            string[] encodingResponce = responceFromServer.Split("=|=");


            lock (locker)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Ответ от сервера:");
                
                foreach (var item in encodingResponce)
                {
                    Console.WriteLine(item);
                }

                Console.ResetColor();
            }
        }
    }

}
