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
        private ListBox listBox;

        public AsyncClient(ListBox listB) 
        {
            listBox = listB;
        }

        private void ConsoleWriteErorr(string erorrText)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(erorrText);
            Console.ResetColor();
        }

        public void SendMessage(string ip, int port, string message)
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
        private void ConnectCallback(IAsyncResult ar)
        {
            ClientState clientState = (ClientState)ar.AsyncState;

            ///send data to server
            Send(clientState);

        }


        private void Send(ClientState state)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(state.Message);
            state.Socket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(SendMessageEndCallBack), state);
        }

        //Callback method при успешной передаче всех данніх на сервер
        private void SendMessageEndCallBack(IAsyncResult ar)
        {
            ClientState state = (ClientState)ar.AsyncState;
            Socket server = state.Socket;

            ///ПОлучаем ответ от сервера
            ReciveResponce(state);
        }

        /// <summary>
        /// Получает ответ от сервера
        /// </summary>
        private void ReciveResponce(ClientState state)
        {
            state.ByteMessageResponce = new byte[1024];
            state.Socket.BeginReceive(state.ByteMessageResponce, 0, state.ByteMessageResponce.Length, SocketFlags.None, new AsyncCallback(ReciveResponceCallBack), state);
        }

        /// <summary>
        /// Колбек функция управление которой будет передано при получении ответа от сервера
        /// </summary>
        /// <param name="ar"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void ReciveResponceCallBack(IAsyncResult ar)
        {
            ClientState state = (ClientState)ar.AsyncState;
            Socket socket = state.Socket;


            int lenUploadetByte = socket.EndReceive(ar);

            string responceFromServer = Encoding.UTF8.GetString(state.ByteMessageResponce, 0, lenUploadetByte);

            string[] encodingResponce = responceFromServer.Split("=|=");
            
            foreach (var item in encodingResponce)
            {
                listBox.Items.Add(item);
            }
            

        }
        }
    }


