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
        ArrayList Players { get; set; }
        ArrayList RoundManagers { get; set; }

        public Game(Pool pool)
        {
            Players = GeneratePlayerManagers(pool);
            Deck = GenerateDeck();
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

        private void GetCardsFromClient(PacketHeader packetHeader, Connection connection, string json)
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
                CardManagers.Sort();
                PlayerManager playerManager = (PlayerManager) Players[playerNumber];
                playerManager.Hand = new Hand { CardManagers = CardManagers };
            }

            Console.WriteLine("Cards successfully dispensed !");
            return true;
        }

        bool SwapCardsByRank()
        {
            ((PlayerManager)Players[0]).Player.Rank = PlayerRank.Asshole;
            ((PlayerManager)Players[1]).Player.Rank = PlayerRank.ViceAsshole;
            ((PlayerManager)Players[2]).Player.Rank = PlayerRank.VicePresident;
            ((PlayerManager)Players[3]).Player.Rank = PlayerRank.President;
            int[] playerRanksNumbers = new int[5] { -1, -1, -1, -1, -1 }; // position in the array = rank of the player, value = number of the player;
            for (int playerNumber = 0; playerNumber < Players.Count; playerNumber ++)
            {
                PlayerManager player = (PlayerManager) Players[playerNumber];
                if (player.Player.Rank != PlayerRank.Neutral);
                playerRanksNumbers[(int) player.Player.Rank] = playerNumber;
            }
            if (Players.Count > 3)
            {
                SwapCards(2, (int) playerRanksNumbers[(int) PlayerRank.President], (int) playerRanksNumbers[(int) PlayerRank.Asshole]);
                SwapCards(1, (int) playerRanksNumbers[(int) PlayerRank.VicePresident], (int) playerRanksNumbers[(int) PlayerRank.ViceAsshole]);
            }
            return true;
        }

        bool SwapCards(int numberOfCardsToSwap, int ElPresidenteNumber, int ElPendejoNumber)
        {
            PlayerManager ElPresidente = (PlayerManager) Players[ElPresidenteNumber];
            PlayerManager ElPendejo = (PlayerManager) Players[ElPendejoNumber];
            Console.WriteLine("Before :");
            Console.WriteLine(ElPresidente);
            Console.WriteLine(ElPendejo);
            CardManager[] ElPresidenteCards = new CardManager[numberOfCardsToSwap];
            CardManager[] ElPendejoCards = new CardManager[numberOfCardsToSwap];
            ElPresidenteCards = (CardManager[]) ElPresidente.Hand.CardManagers.GetRange(ElPresidente.Hand.CardManagers.Count - 1 - numberOfCardsToSwap, numberOfCardsToSwap).ToArray(typeof(CardManager));
            ElPendejoCards = (CardManager[]) ElPendejo.Hand.CardManagers.GetRange(0, numberOfCardsToSwap).ToArray(typeof(CardManager));
            ElPendejo.Hand.CardManagers.AddRange(ElPresidenteCards);
            ElPresidente.Hand.CardManagers.RemoveRange(ElPresidente.Hand.CardManagers.Count - 1 - numberOfCardsToSwap, numberOfCardsToSwap);
            ElPresidente.Hand.CardManagers.AddRange(ElPendejoCards);
            ElPendejo.Hand.CardManagers.RemoveRange(0, numberOfCardsToSwap);
            ElPresidente.Hand.CardManagers.Sort();
            ElPendejo.Hand.CardManagers.Sort();
            Console.WriteLine("After :");
            Console.WriteLine(ElPresidente);
            Console.WriteLine(ElPendejo);
            return true;
        }

        bool SendCards()
        {
            Console.WriteLine("Sending cards ...");
            foreach (PlayerManager playerManager in Players)
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
            // SwapCardsByRank();
            SendCards();
            
            
        }
    }
}
