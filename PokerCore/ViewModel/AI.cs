using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using ReactiveUI;

namespace PokerCore.ViewModel
{
    class AI
    {
        private ITableStateForPlayer table;

        private double TrustRatio = 0.5;

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

        }
    }
}
