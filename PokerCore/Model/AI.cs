using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using ReactiveUI;
using PokerCore.Model;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace PokerCore.ViewModel
{
    public class AI : Player
    {
        
        Dictionary<int, double> _aggresivity;
        double _myAggresive = 0.5;
        double _firstChoiseKoef, _secondChoiseKoef, _thirdChoiseKoef;

        private double _trustRatio = 0.5;

        private static Random random = new Random();
        //gauss params
        public AI(string name, int cash, Poker pok) : base(name, cash, pok)
        {
            MyState = new PlayerState(name, cash);
            _table = new TableForPlayer(pok);

        }

        private int GetOuts(List<Card> cards)
        {
            //все карты, что усилят руку
            return _table.countOuts(cards);
        }

        private int GetDiscountOuts(List<Card> cards)
        {
            //все карты, что усилят только нашу руку
            return _table.countOuts(cards.GetRange(0, 2));
        }

        private double GetOdds(List<Card> cards)
        {
            //отношение бесполезных карт к аутам
            int invalidCards;
            switch (_table.CurTurn)
            {
                case 0:
                    invalidCards = 52 - 2;
                    break;
                case 1:
                    invalidCards = 52 - 5;
                    break;
                case 2:
                    invalidCards = 52 - 6;
                    break;
                case 3:
                    invalidCards = 52 - 7;
                    break;
                default:
                    invalidCards = 50;
                    break;
            }
            return invalidCards/(double)GetOuts(cards);
        }

        private double GetPotOdds()
        {
            //отношение текущего банка к текущей ставке
            return _table.AllBank/_table.CurrentBet;
        }

        private double GenGaussNumber(double E, double D)
        {
            return random.NextDouble();
        }

        private Card NextCard { get => new Card((CardRank)(random.Next(13)), (CardSuit)(random.Next(4))); }

        private bool StateEnd()
        {
            return true;
        }

        private void MakeTurn()
        {
            int nThreads = 100;
            //моделирование многих ситуаций
            Parallel.For(0, nThreads, ind =>
            {
                //инициализация
                List<(int, int)> threadBanks;
                Dictionary<int, (Card, Card)> Cards = new Dictionary<int, (Card, Card)>();
                SortedDictionary<int, Player> PlayersInfo = new SortedDictionary<int, Player>();//плохо
                GameTurn curState;
                CycleList playersOrder = new CycleList();

                threadBanks = _table.Banks;
                curState = GameTurn.preFlop;//придумать как выбрать текущую
                PlayersInfo = _table.Players;
                foreach (var item in PlayersInfo)//плохо
                {
                    if (item.Key == MyState.MySeat)
                        Cards[item.Key] = MyState.HandCards;
                    else
                        Cards[item.Key] = (NextCard, NextCard);
                }
                playersOrder.AddRange(PlayersInfo.SkipWhile(x => x.Key != MyState.MySeat).Select(x => x.Key).ToList());
                playersOrder.AddRange(PlayersInfo.TakeWhile(x => x.Key != MyState.MySeat).Select(x => x.Key).ToList());
                var curPlayer = playersOrder.GetNode(MyState.MySeat);
                //поехали моделировать
                while (curState != GameTurn.River)
                {
                    while (StateEnd())
                    {


                        curPlayer = curPlayer.Next;
                    }
                }
            });
        }

        private double GetWinProb()
        {
            return 0;
        }

        public void GetOptimalMove(List<Card> cards)
        {
            double k = 2;
            if (GetPotOdds() > GetOdds(cards))
                if (_table.CurrentBet == 0)
                    Check();
                else
                    Fold();
            else
                if (k * GetDiscountOuts(cards) > GetOuts(cards))
                Call();
            else
            {
                int raise = GetOptimalRaise(cards);
                if (raise < _table.CurrentRaise)
                    if (MyState.Cash < _table.CurrentBet)
                        Fold();
                    else
                        Call();
                else
                    Raise(raise);
            }
        }


        public int GetOptimalRaise(List<Card> cards)
        {
            int outs = GetOuts(cards);

            int res = 0, cash = MyState.Cash;

            switch (outs)
            {
                case 7:
                    res = (int)(cash * 0.1);
                    break;
                case 8:
                    res = (int)(cash * 0.15);
                    break;
                case 9:
                    res = (int)(cash * 0.2);
                    break;
                case 10:
                    res = (int)(cash * 0.3);
                    break;
            }
            if (outs > 10)
            {
                res = (int)(cash * 0.5);
            }
            return res;
        }
    }
}
