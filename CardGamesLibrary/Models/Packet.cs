using CardGamesLibrary.Models.Card;
using Newtonsoft.Json;
using ProtoBuf;

namespace CardGamesLibrary.Models.NetworkPacket
{

    public static class NetworkPacketHeader
    {
        public const string SET_TURN = "SET_TURN";
        public const string ROUND_START = "ROUND_START";
        public const string SEND_CARDS = "SEND_CARDS";
        public const string CARDS_PLAYED = "CARDS_PLAYED";
        public const string END = "END";
    }
}
