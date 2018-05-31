using CardGamesLibrary.Models.Card;
using Newtonsoft.Json;
using ProtoBuf;

namespace CardGamesLibrary.Models.NetworkPacket
{
    public class NetworkPacket {
        public string game = "belote";
    }

    public static class NetworkPacketHeader
    {
        public const string SET_TURN = "SET_TURN";
        public const string BID = "BID";
        public const string PASS = "PASS";
        public const string END = "END";
        public const string UPDATE = "UPDATE";
        public const string SEND_CARD = "SEND_CARD";
        public const string SCORE = "SCORE";
    }
}
