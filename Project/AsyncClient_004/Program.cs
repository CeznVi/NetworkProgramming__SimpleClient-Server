using System;

namespace AsyncClient_004
{

    internal class Program
    {
        static void Main(string[] args)
        {

            Random rnd = new Random();

            for (int i = 0; i < 10; i++)
            {
                Task.Run(() =>
                {
                    AsyncClient.SendMessage("127.0.0.1", 49000, $"Привет сервер, я T-{rnd.Next(0, 100)}, нужно обновление прошивки.");
                });
            }

            Console.ReadKey();
            

        }
    }
}