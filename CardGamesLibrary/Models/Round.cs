using CardGamesLibrary.Models.Card;
using CardGamesLibrary.Models.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGamesLibrary.Models.Round
{
    public class RoundModel
    {
        public PlayerRank Rank { get; set; }
        public int Number { get; set; }
        public CardModel[] RecievedCards { get; set; }
        public CardModel[] RemovedCards { get; set; }
    }
}
