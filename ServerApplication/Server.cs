using System;
using CardGamesLibrary.Models.NetworkPacket;
using CardGamesLibrary.Models.Card;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using Newtonsoft.Json;

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
            Console.WriteLine("Client connected : " + connection.ToString());
            this.pool.AddConnection(connection);
            if (this.pool.GetSize() == 1)
            {
                int position = 0;
                while (position < 1)
                {
                    Console.WriteLine("sending card to client number: " + position);
                    Connection connection2 = this.pool.getConnection(position);
                    Console.WriteLine(connection2.ToString());
                    CardModel card = new CardModel(10, CardColor.Diamonds, 10, true);
                    Console.WriteLine("card: " + card.ToString());
                    connection2.SendObject(NetworkPacketHeader.SEND_CARD, JsonConvert.SerializeObject(card));
                    position++;
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
