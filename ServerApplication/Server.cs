using System;
using CardGamesLibrary.Models.Card;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using CardGamesLibrary;

namespace ServerApplication
{
    class Server
    {
        private Pool pool = new Pool();

        public Server(string IpAddress, int port)
        {
            Console.WriteLine("Starting server on port : " + IpAddress + ":" + port);
            NetworkComms.AppendGlobalConnectionEstablishHandler(ClientConnected);
            NetworkComms.AppendGlobalConnectionCloseHandler(ClientDisconnected);
            Connection.StartListening(ConnectionType.TCP, new System.Net.IPEndPoint(System.Net.IPAddress.Parse(IpAddress), port));
            Console.WriteLine("\nPress q to quit or any other key to continue.");
            while (true)
            {
                if (Console.ReadKey(true).Key == ConsoleKey.Q) break;
            }

            NetworkComms.Shutdown();
        }

        void ClientConnected(Connection connection)
        {
            if (this.pool.GetSize() == Pool.MAX_SIZE)
            {
                connection.CloseConnection(false);
            }
            else
            {
                Console.WriteLine("Client connected : " + connection.ToString());
                this.pool.AddConnection(connection);
                if (this.pool.GetSize() == Pool.MAX_SIZE)
                {
                    Game game = new Game(this.pool);
                    game.Start();
                }
            }
        }

        void ClientDisconnected(Connection connection)
        {
            Console.WriteLine("Client disconnected : " + connection.ToString());
            this.pool.RemoveConnection(connection);
        }
    }
}