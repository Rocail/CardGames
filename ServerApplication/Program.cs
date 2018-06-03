using System;

namespace ServerApplication
{
    internal class Program
    {
        private static int Main(string[] args)
        {
            try
            {
                new Server("127.0.0.1", 10000);

            } catch(Exception e)
            {
                Console.WriteLine("An error occured : " + e.Message);
                Console.WriteLine("Server closed");
                return(84);
            }
            Console.ReadKey();
            return(0);
        }
    }
}