using System;
using System.Collections.Generic;
using System.Text;

namespace PokerCore.Model
{
    public class TableBase : ITableBase
    {
        Dictionary<int, PlayerState> _players;
        public Dictionary<int, IPlayerState> Players { get; }

        List<Card> _boardCards;
        public IEnumerable<ICard> BoardCards { get => _boardCards; }

        int _dealer;
        public int Dealer { get => _dealer; }

        int _curPlayer;
        public int CurPlayer { get => _curPlayer; set { _curPlayer = value; } }

        int _smallBlind;
        public int SmallBlind { get => _smallBlind; }

        int _bigBlind;
        public int BigBlind { get => _bigBlind; }

        int _currentRaise;
        public int CurrentRaise { get => _currentRaise; set { _currentRaise = value; } }

        int _currentBet;
        public int CurrentBet { get => _currentBet; set { _currentBet = value; } }

        int _bank;
        public int Bank { get => _bank; set { _bank = value; } }

        int _bank2;
        public int Bank2 { get => _bank2; set { _bank2 = value; } }
    }
}
