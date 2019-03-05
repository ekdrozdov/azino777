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
        enum gameTurn { preFlop, Flop, Tern, River, Kciker }

        Dictionary<int, double> _aggresivity;
        double _myAggresive = 0.5;

        double _firstChoiseKoef, _secondChoiseKoef, _thirdChoiseKoef;

        private double _trustRatio = 0.5;

        private static Random random = new Random();
        //gauss params
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
                List<int> threadBanks;
                Dictionary<int, (Card, Card)> Cards = new Dictionary<int, (Card, Card)>();
                Dictionary<int, PlayerState> PlayersInfo = new Dictionary<int, PlayerState>();//плохо
                gameTurn curState;
                List<int> playersOrder;

                threadBanks = _table.Banks;
                curState = gameTurn.preFlop;//придумать как выбрать текущую
                PlayersInfo = _table.Players;
                playersOrder = new List<int>(PlayersInfo.Count);
                foreach (var item in PlayersInfo)//плохо
                {
                    if (item.Key == MyState.MySeat)
                        Cards[item.Key] = HandCards;
                    else
                        Cards[item.Key] = (NextCard, NextCard);
                }
                playersOrder.AddRange(PlayersInfo.SkipWhile(x => x.Key != MyState.MySeat).Select(x => x.Key).ToList());
                playersOrder.AddRange(PlayersInfo.TakeWhile(x => x.Key != MyState.MySeat).Select(x => x.Key).ToList());
                int i, n = playersOrder.Count;
                //поехали моделировать
                while (curState != gameTurn.Kciker)
                {
                    i = 0;
                    while (StateEnd())
                    {
                        PlayersInfo[playersOrder[i % n]].


                    }
                }
                //
            });
        }

        private double GetWinProb()
        {
            return 0;
        }

        private void UpdateAggresivity()
        {

        }

    }
}
