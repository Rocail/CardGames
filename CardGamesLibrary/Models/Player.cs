using CardGamesLibrary.Models.Card;
using ProtoBuf;
using System.Collections.Generic;

namespace CardGamesLibrary.Models.Player
{
    [ProtoContract]
    public class PlayerModel 
    {
        // public List<CardModel> cards;     // CardModel
        [ProtoMember(1)]
        public int score;
        [ProtoMember(2)]
        public int position;              // PlayerPosition


        PlayerModel() { }
        public PlayerModel(int score, int position)
        {
            this.score = score;
            this.position = position;
        }
    }

    public static class PlayerPosition
    {
        public const int NORTH = 0;
        public const int EAST = 1;
        public const int SOUTH = 2;
        public const int WEST = 3;
    }
}
