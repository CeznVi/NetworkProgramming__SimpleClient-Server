using MessageServer.Entity;

namespace ConsoleClientForTest
{
    internal class Program
    {
        static void Main(string[] args)
        {

            AsyncClient.SendMessage("127.0.0.1", 49000, $"sent_message=|=user=|=TEST=|=Lorem ipsum asdasdasdadasdasda");
            
            AsyncClient.SendMessage("127.0.0.1", 49000, $"Get_message=|=user");

            Console.ReadKey();
        }
    }
}