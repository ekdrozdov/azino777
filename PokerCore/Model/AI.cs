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

        private int GetOuts(List<ICard> cards)
        {
            //все карты, что усилят руку
            return _table.countOuts(cards);
        }

        private int GetDiscountOuts(List<ICard> cards)
        {
            //все карты, что усилят только нашу руку
            return _table.countOuts(cards.GetRange(0, 2));
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
                List<(int, int)> threadBanks;
                Dictionary<int, (Card, Card)> Cards = new Dictionary<int, (Card, Card)>();
                Dictionary<int, Player> PlayersInfo = new Dictionary<int, Player>();//плохо
                gameTurn curState;
                CycleList playersOrder = new CycleList();

                threadBanks = _table.Banks;
                curState = gameTurn.preFlop;//придумать как выбрать текущую
                PlayersInfo = _table.Players;
                foreach (var item in PlayersInfo)//плохо
                {
                    if (item.Key == MyState.MySeat)
                        Cards[item.Key] = HandCards;
                    else
                        Cards[item.Key] = (NextCard, NextCard);
                }
                playersOrder.AddRange(PlayersInfo.SkipWhile(x => x.Key != MyState.MySeat).Select(x => x.Key).ToList());
                playersOrder.AddRange(PlayersInfo.TakeWhile(x => x.Key != MyState.MySeat).Select(x => x.Key).ToList());
                var curPlayer = playersOrder.GetNode(MyState.MySeat);
                //поехали моделировать
                while (curState != gameTurn.Kciker)
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

        private void UpdateAggresivity()
        {

        }

        public GameState GetOptimalMove(List<ICard> cards)
        {
            int outs = GetOuts(cards);
            if (outs >= 7)
            {
                return GameState.raise;
            }

            if (cards.Count == 5 && outs <= 6)
            {
                if (cards[0].Rank < CardRank.J && cards[1].Rank < CardRank.J)
                {
                    return GameState.fold;
                }
            }

            return GameState.call;
        }





    }
}
