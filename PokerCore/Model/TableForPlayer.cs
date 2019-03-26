using PokerCore.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokerCore.Model
{
    public class TableForPlayer
    {
        protected Poker _playerView;

        public TableForPlayer(Poker playerView)
        {
            _playerView = playerView;
        }
        public int countOuts(List<Card> cards) => _playerView.countOuts(cards);

        public int Dealer { get => _playerView.Dealer; }

        public int CurPlayer { get => _playerView.CurPlayer; }

        public int SmallBlind { get => _playerView.SmallBlind; }

        public int BigBlind { get => _playerView.BigBlind; }

        public int FirstRaiser { get => _playerView.FirstRaiser; set => _playerView.FirstRaiser = value; }

        public int CurrentRaise { get => _playerView.CurrentRaise; set => _playerView.CurrentRaise = value; }

        public int CurrentBet { get => _playerView.CurrentBet; set => _playerView.CurrentBet = value; }

        public int AllBank { get => _playerView.AllBank; set => _playerView.AllBank = value; }

        public List<(int, int)> Banks { get => _playerView.DividedBanks; set => _playerView.DividedBanks = value; }

        public SortedDictionary<int, Player> Players { get => _playerView.Players; }

        public List<string> BoardCards { get => _playerView.BoardCards; }

        public (Card, Card) HandCards { get => _playerView.Players[CurPlayer].MyState.HandCards; }

        public int CurTurn { get => _playerView.CurTurn; }

        public void AddBank(int bankBeforeBet)
        {
            _playerView.DividedBanks.Add((_playerView.CurPlayer, bankBeforeBet));
        }
    }
}

