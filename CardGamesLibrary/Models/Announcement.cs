using ProtoBuf;

namespace CardGamesLibrary.Models.Announcement
{
    [ProtoContract]
    class AnnouncementModel
    {
        [ProtoMember(1)]
        public int type;                // Announcement.Type
        [ProtoMember(2)]
        public int color;               // Card.Color
        [ProtoMember(3)]
        public int value;
        [ProtoMember(4)]
        public int highestRank;         // Card.Rank
        [ProtoMember(5)]
        public int playerPosition;      // Player.Position

        AnnouncementModel() {}
        public AnnouncementModel(int type, int color, int value, int highestRank, int playerPosition)
        {
            this.type = type;
            this.color = color;
            this.value = value;
            this.highestRank = highestRank;
            this.playerPosition = playerPosition;
        }
    }

    public static class AnnouncementType
    {
        public const int Carre = 0;
        public const int Sequence = 1;
        public const int Belote = 2;

    }
}
