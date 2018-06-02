using CardGamesLibrary.Models.Player;
using NetworkCommsDotNet.Connections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApplication.GameObjects
{
    class PlayerManager
    {
        public PlayerModel Player { get; set; }
        public Connection Connection { get; set; }
        public Hand Hand { get; set; } = new Hand();

        override public string ToString()
        {
            return Player.ToString() + "\n" +
                Hand.ToString();
        }
    }
}
