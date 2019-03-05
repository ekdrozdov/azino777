using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using ReactiveUI;
using PokerCore.Model;

namespace PokerCore.ViewModel
{
    public class AI : Player
    {
        enum gameState { preFlop, Flop, Tern, River, Kciker }
        enum playerState { fold, check, call, raise }

        Dictionary<int, double> _aggresivity;
        double _myAggresive = 0.5;

        double _firstChoiseKoef, _secondChoiseKoef, _thirdChoiseKoef;

        private double _trustRatio = 0.5;

        public AI(string name, int cash) : base(name, cash)
        {
        }

        private double GetOuts()
        {
            //все карты, что усилят руку
            return 0;
        }

        private double GetDiscountOuts()
        {
            //все карты, что усилят только нашу руку
            return 0;
        }

        private double GetOdds()
        {
            //отношение бесполезных карт к аутам
            return 0;
        }

        private double GetPotOdds()
        {
            //отношение текущего банка к текущей ставке
            return 0;
        }


        private void MakeTurn()
        {
            //моделирование многих ситуаций
            Dictionary<int, (Card, Card)> Cards;
            Dictionary<int, (PlayerState, int)> PlayersInfo;
            gameState curState;

        }

        private double GetWinProb()
        {

        }

        private void UpdateAggresivity()
        {

        }

    }
}
