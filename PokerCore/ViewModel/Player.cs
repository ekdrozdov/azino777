using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Reactive;
using ReactiveUI;
using System.Linq;
using PokerCore.Model;
using PokerCore.Model.DataBase;
using System.ComponentModel;

namespace PokerCore.ViewModel
{
    public class Player : ReactiveObject
    {
        PlayerState _myState;
        public PlayerState MyState { get => _myState; set { _myState = value; OnPropertyChanged("MyState"); } }

        (Card, Card) _handCards;
        public (Card, Card) HandCards { get => _handCards; set { _handCards = value; OnPropertyChanged("HandCards"); } }

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

            AddInDb("Fold");
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

            AddInDb("Call");
        }

        public void Check()
        {
            if (_table.CurrentBet == 0)
                _myState.PlayerBet = 0;
            else
                throw new Exception("Вы не можете сделать чек, ставки уже сделаны.");

            AddInDb("Check");
        }

        public void Raise(int raise)
        {
            if (raise > _table.CurrentRaise)
            {
                int BetDifferenсe = _table.CurrentBet - _myState.PlayerBet;
                if (_myState.Cash > BetDifferenсe + raise)
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

            AddInDb("Raise");
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

            AddInDb("AllIn");
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

            AddInDb("Bet");
        }

        //получение названия текущего раунда для добавления в бд
        public string GetRoundName()
        {
            string RoundName = "";

            if (_table.BoardCards.Count == 0)
                RoundName = "Pre-flop";
            if (_table.BoardCards.Count == 3)
                RoundName = "Flop";
            if (_table.BoardCards.Count == 4)
                RoundName = "Turn";
            if (_table.BoardCards.Count == 3)
                RoundName = "River";

            return RoundName;
        }

        //добавить запись об одном ходе раунда в бд
        public void AddInDb(string _actionName)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                DBGame game = db.Games.Last();
                DBPlayer player = db.Players.Where(p => p.Name == _myState.Name).Last();

                db.Rounds.Add(new DBRound
                {
                    Name = GetRoundName(),
                    Game = game.Id,
                    Player = player.Id,
                    ActionName = _actionName,
                    BetSize = _myState.PlayerBet
                    //DecisionTime
                });
                db.SaveChanges();
            }
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
