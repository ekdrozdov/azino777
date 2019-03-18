using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokerCore.Model
{
    public class PlayerState : ReactiveObject
    {
        public PlayerState(string name, int cash) { _name = name; _cash = cash; HandCards = (new Card(CardRank.Q, CardSuit.Diamonds), new Card(CardRank.c4, CardSuit.Spades)); }

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

        (Card, Card) _handCards;
        public (Card, Card) HandCards { get => _handCards; set => this.RaiseAndSetIfChanged(ref _handCards, value); }
        public string FirstCardName { get => _handCards.Item1.GetTextureName(); }
        public string SecondCardName { get => _handCards.Item2.GetTextureName(); }
    }
}
