using System;
using System.Collections.Generic;
using System.Text;

namespace PokerCore.Model
{
    public class TableForPlayer
    {
        PokerM _playerView;

        public TableForPlayer()
        {

        }
        public int Dealer { get => _playerView.Dealer; }

        public int CurPlayer { get; }

        public int SmallBlind { get; }

        public int BigBlind { get; }

        public int CurrentRaise { get; set; }

        public int CurrentBet { get; set; }

        public List<int> Banks { get; set; }

        public Dictionary<int, PlayerState> Players { get; }

        public List<ICard> BoardCards { get; }

        public (ICard, ICard) HandCards { get; }

        public void AddBank(int cash)
        {

        }
    }
}
