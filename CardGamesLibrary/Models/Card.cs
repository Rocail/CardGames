using ProtoBuf;

namespace CardGamesLibrary.Models.Card
{
    public class CardModel
    {
        public CardRank Rank { get; set; }
        public CardColor Color { get; set; }
        
        override public string ToString()
        {
            string message = "";
            message = Rank + " of " + Color;
            return(message);
        }
    }

    public enum CardRank
    {
         Ace = 14,
         Two = 15,
         Three = 3,
         Four = 4,
         Five = 5,
         Six = 6,
         Seven = 7,
         Height = 8,
         Nine = 9,
         Ten = 10,
         Jack = 11,
         Queen = 12,
         King = 13
    }

    public enum CardColor
    {
        Spades = 0,
        Hearts = 1,
        Diamonds = 2,
        Clubs = 3
    }
}
