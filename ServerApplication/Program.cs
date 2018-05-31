namespace ServerApplication
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            new Server("127.0.0.1", 10000);
        }
    }
}
