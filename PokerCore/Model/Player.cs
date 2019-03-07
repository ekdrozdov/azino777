using System;
using System.Collections.Generic;
using System.Text;
using System.Reactive;

namespace PokerCore.Model
{
    public class Player
    {
        PlayerState _myState;
        public PlayerState MyState { get; set; }

        (Card, Card) _handCards;
        public (Card, Card) HandCards { get => _handCards; set { _handCards = (value); } }

        TableForPlayer _table;

        public Player(string name, int cash)
        {
            //Name = name;
            _myState.Cash = cash;
        }

        public void AddCash(int cash)
        {
            _myState.Cash = cash;
        }

        public void Fold()
        {
            _myState.PlayerBet = 0;
        }

        public Unit Call(int bet)
        {
            try
            {
                int BetDifferense = bet - _myState.PlayerBet;
                if (_myState.Cash >= BetDifferense)
                {
                    _myState.Cash -= BetDifferense;
                    _myState.PlayerBet += BetDifferense;
                }
            }
            catch {
                // "У вас недостаточно средств, чтобы сделать ставку"
            }
            
        }

        public void Check(int bet)
        {
            if (bet == 0) _myState.PlayerBet = 0;
        }

        public void Raise(int raise)
        {
            _myState.PlayerBet = raise;
        }

        public void AllIn()
        {
            _myState.PlayerBet = _myState.Cash;
        }
    }
}
