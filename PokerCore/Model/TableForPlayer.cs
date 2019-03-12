using PokerCore.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokerCore.Model
{
    public class TableForPlayer
    {
        Poker _playerView;

        public TableForPlayer(Poker playerView)
        {
            _playerView = playerView;
        }
        public int Dealer { get => _playerView.Dealer; }

        public int CurPlayer { get => _playerView.CurPlayer; }

        public int SmallBlind { get => _playerView.SmallBlind; }

        public int BigBlind { get => _playerView.BigBlind; }

        public int CurrentRaise { get => _playerView.CurrentRaise; set => _playerView.CurrentRaise = value;  }

        public int CurrentBet { get => _playerView.CurrentBet; set => _playerView.CurrentBet = value;  }

        public int AllBank { get => _playerView.AllBank; set => _playerView.AllBank = value; }

        public List<(int, int)> Banks { get => _playerView.DividedBanks; set => _playerView.DividedBanks = value; }
        
        public Dictionary<int, Player> Players { get => _playerView.Players; }

        public List<Card> BoardCards { get => _playerView.BoardCards; }

        public (Card, Card) HandCards { get => _playerView.Players[CurPlayer].HandCards; }

        public void AddBank(int bankBeforeBet)
        {
            _playerView.DividedBanks.Add((_playerView.CurPlayer, bankBeforeBet));
        }
    }
}
