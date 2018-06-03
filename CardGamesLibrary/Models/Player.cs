using CardGamesLibrary.Models.Card;
using ProtoBuf;
using System.Collections.Generic;

namespace CardGamesLibrary.Models.Player
{
    public class PlayerModel 
    {
        public PlayerRank Rank { get; set; }
        public int Number { get; set; }

        public override string ToString()
        {
            return "Player number " + Number + ", rank " + Rank;
        }
    }

    public enum PlayerRank
    {
        President,
        VicePresident,
        Neutral,
        ViceAsshole,
        Asshole
    }
}