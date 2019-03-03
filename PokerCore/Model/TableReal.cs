using System;
using System.Collections.Generic;
using System.Text;

namespace PokerCore.Model
{
    public class TableReal : ITableReal
    {
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

        CardDeck _deck;
        public ICardDeck Deck { get; }

        public IEnumerable<int> GetStrongestCombination()
        {
            List<int> smth = new List<int>() { 1, 2 };
            return smth;
        }

        Dictionary<int, Card> _handCards;
        public Dictionary<int, ICard> HandCards { get; }

        GameRules _rules;
        public IGameRules Rules { get; }

        public void TryConnect(string name)
        {

        }
    }
}
