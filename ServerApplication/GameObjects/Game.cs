using CardGamesLibrary.Models.Card;
using CardGamesLibrary.Models.Player;
using CardGamesLibrary.Models.NetworkPacket;
using NetworkCommsDotNet.Connections;
using ServerApplication.GameObjects;
using System;
using System.Collections;
using CardGamesLibrary;
using NetworkCommsDotNet;

namespace ServerApplication
{
    public class Game
    {
        ArrayList Deck { get; set; }
        ArrayList PlayerManagers { get; set; }

        public Game(Pool pool)
        {
            PlayerManagers = GeneratePlayerManagers(pool);
            Deck = GenerateDeck();
            InitiateConnectionHandlers();
        }

        ArrayList GeneratePlayerManagers(Pool pool)
        {
            Console.WriteLine("Generating players ...");
            ArrayList PlayerManagers = new ArrayList();
            Console.WriteLine("Pool size :" + pool.GetSize());
            for (int count = 0; count < Pool.MAX_SIZE; count++)
            {
                PlayerModel playerModel = new PlayerModel { Number = count, Rank = PlayerRank.Neutral };
                PlayerManager playerManager = new PlayerManager() { Player = playerModel, Connection = pool.getConnection(count) };
                PlayerManagers.Add(playerManager);
                Console.WriteLine("Player added : " + playerManager.ToString());
            }
            Console.WriteLine("Players successfullly generated !");
            return PlayerManagers;
        }

        ArrayList GenerateDeck()
        {
            Console.WriteLine("Generating deck ...");
            ArrayList deck = new ArrayList();
            Random random = new Random();
            for (CardRank rank = CardRank.Three; rank <= CardRank.Two; rank++)
            {
                for (CardColor color = CardColor.Spades; color <= CardColor.Clubs; color++)
                {
                    deck.Insert(random.Next(0, deck.Count), new CardModel() { Rank = rank, Color = color });
                }
            }
            Console.WriteLine("Deck successfullly generated !");
            return deck;
        }

        public bool InitiateConnectionHandlers()
        {
            foreach (PlayerManager playerManager in PlayerManagers)
            {
                playerManager.Connection.AppendIncomingPacketHandler<string>(NetworkPacketHeader.SEND_CARDS, GetCardsFromClient);
            }
            return true;
        }

        private void GetCardsFromClient(PacketHeader packetHeader, Connection connection, string incomingObject)
        {
            
        }

        bool DispenseCards()
        {
            Console.WriteLine("Dispensing cards ...");

            int maximumCardsNumber = 52 / Pool.MAX_SIZE;

            for (int playerNumber = 0; playerNumber < Pool.MAX_SIZE; playerNumber++)
            {
                ArrayList CardManagers = new ArrayList();
                for (int count = 0 + (playerNumber * maximumCardsNumber); count < ((playerNumber + 1) * maximumCardsNumber); count++)
                {
                    CardManagers.Add(new CardManager { Card = (CardModel)Deck[count] });
                }
                PlayerManager playerManager = (PlayerManager) PlayerManagers[playerNumber];
                playerManager.Hand = new Hand { CardManagers = CardManagers };
            }

            Console.WriteLine("Cards successfully dispensed !");
            return true;
        }

        bool SendCards()
        {
            Console.WriteLine("Sending cards ...");
            foreach (PlayerManager playerManager in PlayerManagers)
            {
                Connection connection = playerManager.Connection;
                try
                {
                    string json = Serialization.Serialize(playerManager.Hand.GetCardModels());
                    Console.WriteLine("Json : " + json);
                    playerManager.Connection.SendObject(NetworkPacketHeader.SEND_CARDS, json);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    return false;
                }
            }
            Console.WriteLine("Cards sent !");
            return true;
        }

        public void Start()
        {
            DispenseCards();
            SendCards();
        }
    }
}
