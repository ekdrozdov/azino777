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
            _myState.Cash += cash;
        }

        public void Fold()
        {
            _myState.PlayerBet = 0;
            _myState.State = PlayerGameState.Out;
        }

        public Unit Call() 
        {
            try
            {
                int BetDifferense = _table.CurrentBet - _myState.PlayerBet;
                if (_myState.Cash >= BetDifferense)
                {
                    _myState.Cash -= BetDifferense;
                    _myState.PlayerBet += _table.CurrentBet;
                   // _table.Bank += BetDifferense;
                }
            }
            catch {
                //"У вас недостаточно средств, чтобы сделать ставку"
            }
            
        }

        public void Check(int bet)
        {
            if (bet == 0) _myState.PlayerBet = 0;
            else {
                   //"Вы не можете сделать чек, ставки уже сделаны"
            }
        }

        public void Raise(int raise)
        {
            if ( raise > _table.CurrentRaise )
            {
                int BetDifferense =_table.CurrentBet - _myState.PlayerBet;
                if (_myState.Cash> BetDifferense + raise)
                {
                    _myState.Cash -= BetDifferense + raise;
                    //bank
                    _table.CurrentRaise = raise;
                    _table.CurrentBet += raise;
                    _myState.PlayerBet += BetDifferense + raise;
                }
                else
                {
                    //"У вас недостаточно средств, чтобы увеличить размер текущей ставки"
                }
            }
            else
            {
                //"Вы не увеличили размер текущей ставки"
            }
        }

        public void AllIn()
        {
            _table.AllBank += _myState.Cash;
            _myState.PlayerBet += _myState.Cash;
            _myState.Cash = 0;
            _myState.State = PlayerGameState.AllIn;
           // _table.AddBank(bank);
            if (_myState.PlayerBet > _table.CurrentBet)
                _table.CurrentBet = _myState.PlayerBet;
        }

        public void Bet(int bet) 
        {
            if (_table.CurrentBet == 0)
            {
                _myState.Cash -= bet;
                _table.CurrentBet = bet;
                _myState.PlayerBet = bet;               
                //_table.Bank += bet;
            }
        }

    }
}
