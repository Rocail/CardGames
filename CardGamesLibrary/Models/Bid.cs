using ProtoBuf;

namespace CardGamesLibrary.Bid
{
    [ProtoContract]
    public class BidModel
    {
        [ProtoMember(1)]
        public int type;                // BidType
        [ProtoMember(2)]
        public int player_position;     // PlayerPosition
        [ProtoMember(3)]
        public int value;
        [ProtoMember(4)]
        public int trumps;              // CardColor
        [ProtoMember(5)]
        public int coinche;             // BidCoincheType

        BidModel() { }

        public BidModel(int type, int player_position, int value, int trumps, int coinche)
        {
            this.type = type;
            this.player_position = player_position;
            this.value = value;
            this.trumps = trumps;
            this.coinche = coinche;
        }
    }

    public static class BidType
    {
        public const int N = 0;
        public const int Capot = 1;
    }

    public static class BidCoinche
    {
        public const int None = 0;
        public const int Coinche = 1;
        public const int Surcoinche = 2;
    }
}
