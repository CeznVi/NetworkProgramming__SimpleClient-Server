namespace MessageServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Message Server";

            AsyncMessageServer Server = new("127.0.0.1", 49000);
            Server.StartListening();

        }
    }
}