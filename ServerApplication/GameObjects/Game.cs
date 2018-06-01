using CardGamesLibrary.Models.Card;
using System;
using System.Collections;

namespace ServerApplication
{
    public class Game
    {
        Pool pool;
        ArrayList deck;

        public Game(Pool pool)
        {
            this.pool = pool;
            deck = GenerateDeck();
        }


        ArrayList GenerateDeck()
        {
            ArrayList deck = new ArrayList();
            Random random = new Random();
            for ( CardRank rank = CardRank.Three; rank <= CardRank.Two; rank ++)
            {
                for (CardColor color = CardColor.Spades; color < CardColor.Clubs; color++)
                {
                    deck.Insert(random.Next(0, deck.Count), new CardModel() { Rank = rank, Color = color });
                }
            }
            return deck;
        }
    }
}
