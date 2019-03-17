using System;
using System.Collections.Generic;
using System.Text;
using System.Reactive;
using ReactiveUI;
using PokerCore.Model;

namespace PokerCore.ViewModel
{
    public class Player: ReactiveObject
    {
        PlayerState _myState;
        public PlayerState MyState { get => _myState; set => this.RaiseAndSetIfChanged(ref _myState, value); }

        (Card, Card) _handCards;
        public (Card, Card) HandCards { get => _handCards; set => this.RaiseAndSetIfChanged(ref _handCards, value); }

        protected TableForPlayer _table;

        public Player(string name, int cash)
        {
            _myState = new PlayerState(name, cash);
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

        public void Call() 
        {
            int BetDifferenсe = _table.CurrentBet - _myState.PlayerBet;
            if (_myState.Cash >= BetDifferenсe)
            {
                _myState.Cash -= BetDifferenсe;
                _myState.PlayerBet = _table.CurrentBet;
                _table.AllBank += BetDifferenсe;
            }
            else
                throw new Exception("У вас недостаточно средств, чтобы сделать ставку.");
        }

        public void Check()
        {
            if (_table.CurrentBet == 0)
                _myState.PlayerBet = 0;
            else
                throw new Exception("Вы не можете сделать чек, ставки уже сделаны.");
        }

        public void Raise(int raise)
        {
            if ( raise > _table.CurrentRaise )
            {
                int BetDifferenсe =_table.CurrentBet - _myState.PlayerBet;
                if (_myState.Cash> BetDifferenсe + raise)
                {
                    _myState.Cash -= BetDifferenсe + raise;
                    _table.AllBank += BetDifferenсe + raise;
                    _table.CurrentRaise = raise;
                    _table.CurrentBet += raise;
                    _myState.PlayerBet += BetDifferenсe + raise;
                }
                else
                    throw new Exception("У вас недостаточно средств, чтобы увеличить размер текущей ставки.");
            }
            else
                throw new Exception("Вы не увеличили размер текущей ставки.");
        }

        public void AllIn()
        {
            _table.AllBank += _myState.Cash;
            _myState.PlayerBet += _myState.Cash;
            _myState.Cash = 0;
            _myState.State = PlayerGameState.AllIn;
            if (_myState.PlayerBet > _table.CurrentBet)
            {
                _table.AddBank(_table.AllBank);
                _table.CurrentBet = _myState.PlayerBet;
            }
            else
                _table.AddBank(_table.AllBank - _table.CurrentBet + _myState.PlayerBet);
        }

        public void Bet(int bet) 
        {
            if (_table.CurrentBet == 0)
            {
                _myState.Cash -= bet;
                _table.CurrentBet = bet;
                _myState.PlayerBet = bet;               
                _table.AllBank += bet;
            }
            else
                throw new Exception("Cтавка уже была сделана.");
        }
    }
}
