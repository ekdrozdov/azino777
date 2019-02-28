using System;
using System.Collections.Generic;
using System.Text;

namespace PokerCore.Model
{
    class Player : IPlayerState
    {
        public string Name { get; set; }

        public int Cash { get; set; }

        public int PlayerBet { get; set; }

        public int ChairNumber { get; set; }

        public PlayerGameState State { get; set; }
    }
}
