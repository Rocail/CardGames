using System;
using CardGamesLibrary.Models.Card;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using CardGamesLibrary;
using CardGamesLibrary.Models.NetworkPacket;

namespace ServerApplication
{
    class Server
    {
        private Pool pool = new Pool();
        private Game game = null;

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
            if (pool == null)
            {
                connection.CloseConnection(false);
            }
            else
            {
                Console.WriteLine("Client connected : " + connection.ToString());
                connection.AppendIncomingPacketHandler<string>(NetworkPacketHeader.SEND_CARDS, GetCardsFromClient);
                
                pool.AddConnection(connection);
                if (pool.GetSize() == Pool.MAX_SIZE)
                {
                    game = new Game(pool);
                    pool = null;
                    game.Start();
                }
            }
        }

        private void GetCardsFromClient(PacketHeader packetHeader, Connection connection, string incomingObject)
        {
            if (this.game != null)
            {

            }
        }

        void ClientDisconnected(Connection connection)
        {
            if (pool != null)
            {
                Console.WriteLine("Client disconnected : " + connection.ToString());
                try
                {
                    pool.RemoveConnection(connection);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Failed to handle client disconnexion properly");
                    NetworkComms.Shutdown();
                    throw e;
                }
            }
        }
    }
}