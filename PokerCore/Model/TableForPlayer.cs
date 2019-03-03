using System;
using System.Collections.Generic;
using System.Text;

namespace PokerCore.Model
{
    public class TableForPlayer : IPlayer
    {
        public TableForPlayer()
        {

        }

        Dictionary<int, PlayerState> _players;
        public Dictionary<int, IPlayerState> Players { get; }

        List<Card> _boardCards;
        public IEnumerable<ICard> BoardCards { get => _boardCards; }

        int _dealer;
        public int Dealer { get => _dealer; }

        int _curPlayer;
        public int CurPlayer { get => _curPlayer; }

        int _smallBlind;
        public int SmallBlind { get => _smallBlind; }

        int _bigBlind;
        public int BigBlind { get => _bigBlind; }

        int _currentRaise;
        public int CurrentRaise { get => _currentRaise; }

        int _currentBet;
        public int CurrentBet { get => _currentBet; }

        int _bank;
        public int Bank { get => _bank; }

        int _bank2;
        public int Bank2 { get => _bank2; }

        TableBase _tableState;
        public ITableBase TableState { get => _tableState; }

        List<Card> _handCards;
        public IEnumerable<ICard> HandCards { get => _handCards; }

        public void SetName()
        { }
        public void AddCash(int cash)
        { }
        public void Fold()
        { }
        public Unit Call()
        { return new Unit(); }
        public void Check()
        {

        }
        public void Raise(int raise)
        { }
        public void AllIn()
        { }

    }
}
