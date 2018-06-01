using System;
using CardGamesLibrary.Models.NetworkPacket;
using CardGamesLibrary.Models.Card;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using Newtonsoft.Json;
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
            while(true)
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
                    // Game game = new Game(this.pool);
                    int position = 0;
                    while (position < Pool.MAX_SIZE)
                    {
                        Console.WriteLine("sending card to client number: " + position);
                        Connection connection2 = this.pool.getConnection(position);
                        Console.WriteLine(connection2.ToString());
                        CardModel card = new CardModel() { Rank = CardRank.Ten, Color = CardColor.Diamonds };
                        Console.WriteLine("card: " + card.ToString());
                        connection2.SendObject(NetworkPacketHeader.SEND_CARDS, Serialization.Serialize(card));
                        position++;
                    }
                }
            }
        }

        void ClientDisconnected(Connection connection)
        {
            Console.WriteLine("Client diconnected : " + connection.ToString());
            this.pool.RemoveConnection(connection);
        }
    }
}
