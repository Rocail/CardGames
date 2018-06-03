using CardGamesLibrary.Models.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApplication.GameObjects
{
    class CardManager : IComparable
    {
        public CardModel Card { get; set; }

        public int CompareTo(Object OtherCard)
        {
            if (OtherCard.GetType() == typeof(CardManager))
            {
                return Card.Rank.CompareTo(((CardManager) OtherCard).Card.Rank);
            } else
            {
                throw new ArrayTypeMismatchException();
            }
        }
    }
}
