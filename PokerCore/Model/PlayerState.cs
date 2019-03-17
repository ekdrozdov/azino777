using System;
using System.Collections.Generic;
using System.Text;

namespace PokerCore.Model
{
    public class PlayerState
    {
        public PlayerState(string name, int cash) { _name = name; _cash = cash; }

        string _name;
        public string Name { get => _name; set => _name = value; }

        int _cash;
        public int Cash { get => _cash; set => _cash = value;  }

        int _playerBet;
        public int PlayerBet { get => _playerBet; set => _playerBet = value; }

        int _mySeat;
        public int MySeat { get; set; }

        PlayerGameState _state;
        public PlayerGameState State { get => _state; set => _state = value;  }
    }
}
