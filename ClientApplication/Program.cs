using System;
using System.Linq;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using NetworkCommsDotNet.Connections.TCP;
using CardGamesLibrary.Models.NetworkPacket;
using CardGamesLibrary.Models.Card;
using Newtonsoft.Json;
using System.Collections;

namespace ClientApplication
{
    class Program
    {

        static void Main(string[] args)
        {
            string serverInfo = "127.0.0.1:10000";
            string serverIP = serverInfo.Split(':').First();
            int serverPort = int.Parse(serverInfo.Split(':').Last());
    		ConnectionInfo connInfo = new ConnectionInfo(serverIP, serverPort);
            Connection connection = null;

            while (true)
            {
                try
                {
                    connection = TCPConnection.GetConnection(connInfo);
                    connection.AppendIncomingPacketHandler<string>(NetworkPacketHeader.SEND_CARDS, getCardsFromServer);
                    
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.ToString());
                }
                Console.WriteLine("\nPress q to quit or any other key to continue.");
                if (Console.ReadKey(true).Key == ConsoleKey.Q) break;
                try
                {
                    if (connection != null)
                    {
                        connection.CloseConnection(false);
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.ToString());
                }
                Console.WriteLine("\nPress q to quit or any other key to continue.");
                if (Console.ReadKey(true).Key == ConsoleKey.Q) break;

            }

            //We have used comms so we make sure to call shutdown
            connection.CloseConnection(false);
        }

        public static void getCardsFromServer(PacketHeader header, Connection connection, string json) {
            Console.WriteLine(json);
            ArrayList cards = new ArrayList();
            cards.AddRange(JsonConvert.DeserializeObject<CardModel[]>(json));
            Console.WriteLine("Recieved cards from : " + connection.ToString());
            Console.WriteLine("header : " + header.ToString());
            Console.WriteLine("cards : ");
            foreach (CardModel card in cards)
            {
                Console.WriteLine(card.ToString());
            }
            Console.WriteLine("Got " + cards.Count + " cards");
        }
    }
}
