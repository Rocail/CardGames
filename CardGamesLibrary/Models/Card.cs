using ProtoBuf;

namespace CardGamesLibrary.Models.Card
{
    public class CardModel
    {
        public int rank;        // CardRank
        public int color;       // CardColor
        public int value;
        public bool playable;

        protected CardModel() { }

        public CardModel(int rank, int color, int value, bool playable)
        {
            this.rank = rank;
            this.color = color;
            this.value = value;
            this.playable = playable;
        }

        override public string ToString()
        {
            string message = "";
            switch(this.rank)
            {
                case CardRank.Ace:
                    message += "Ace";
                    break;
                case CardRank.Jack:
                    message += "Jack";
                    break;
                case CardRank.Queen:
                    message += "Queen";
                    break;
                case CardRank.King:
                    message += "King";
                    break;
                default:
                    message += this.rank;
                    break;
            }
            message += " of ";
            switch(this.color)
            {
                case CardColor.Spades:
                    message += "spades";
                    break;
                case CardColor.Hearts:
                    message += "hearts";
                    break;
                case CardColor.Diamonds:
                    message += "diamonds";
                    break;
                case CardColor.Clubs:
                    message += "clubs";
                    break;
                default:
                    message += "spoke";
                    break;
            }
            return(message);
        }
    }

    public static class CardRank
    {
        public const int Ace = 1;
        public const int Jack = 11;
        public const int Queen = 12;
        public const int King = 13;
    }

    public static class CardColor
    {
        public const int Spades = 0;
        public const int Hearts = 1;
        public const int Diamonds = 2;
        public const int Clubs = 3;
    }
}
