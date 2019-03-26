using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Reactive;
using ReactiveUI;
using System.Linq;
using PokerCore.Model;
//using PokerCore.Model.DataBase;
using System.ComponentModel;

namespace PokerCore.ViewModel
{
    public class Player : ReactiveObject
    {
        PlayerState _myState;
        public PlayerState MyState { get => _myState; set=> this.RaiseAndSetIfChanged(ref _myState, value); }
        protected TableForPlayer _table;

        public Player(string name, int cash, Poker hz)
        {
            _myState = new PlayerState(name, cash);
            _table = new TableForPlayer(hz);
        }

        public void AddCash(int cash)
        {
            _myState.Cash += cash;
        }

        public void Fold()
        {
            _myState.PlayerBet = 0;
            _myState.State = PlayerGameState.Out;
            _myState.FoldState = true;
            
        }

        public void Call()
        {
            int BetDifferenсe = _table.CurrentBet - _myState.PlayerBet;
            if (_myState.Cash >= BetDifferenсe)
            {
                _myState.Cash -= BetDifferenсe;
                _myState.PlayerBet = _table.CurrentBet;
                _table.AllBank += BetDifferenсe;
                _myState.State = PlayerGameState.In;
            }
            else
                throw new Exception("У вас недостаточно средств, чтобы сделать ставку.");
        }

        public void Check()
        {
            //_myState.PlayerBet = 0;
            _myState.State = PlayerGameState.Check;

        }

        public void Raise(int raise)
        {
            if (raise >= _table.CurrentRaise)
            {
                int BetDifferenсe = _table.CurrentBet - _myState.PlayerBet;
                if (_myState.Cash > BetDifferenсe + raise)
                {
                    _myState.Cash -= BetDifferenсe + raise;
                    _table.AllBank += BetDifferenсe + raise;
                    _table.CurrentRaise = raise;
                    _table.CurrentBet += raise;
                    _myState.PlayerBet += BetDifferenсe + raise;
                    _myState.State = PlayerGameState.In;
                    _table.FirstRaiser = _table.CurPlayer;
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
                if (_myState.PlayerBet - _table.CurrentBet > _table.CurrentRaise)
                {
                    _table.CurrentRaise = _myState.PlayerBet - _table.CurrentBet;
                    _table.FirstRaiser = _table.CurPlayer;
                }
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
                _myState.State = PlayerGameState.In;
            }
            else
                throw new Exception("Cтавка уже была сделана.");
        }

        //получение названия текущего раунда для добавления в бд
        public string GetRoundName()
        {
            string RoundName = "";

            int visibleCardCount = _table.BoardCards.Count;
            if (visibleCardCount == 0)
                RoundName = "Pre-flop";
            if (visibleCardCount == 3)
                RoundName = "Flop";
            if (visibleCardCount == 4)
                RoundName = "Turn";
            if (visibleCardCount == 3)
                RoundName = "River";

            return RoundName;
        }

        public event PropertyChangedEventHandler PropertyChange;

        public virtual void OnPropertyChanged(string propertyName)
        {
            var propertyChanged = PropertyChange;
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
