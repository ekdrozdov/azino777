using System;
using System.Collections.Generic;
using System.Text;

namespace PokerCore.Model
{
    class CardRankCompare: IComparer<ICard>
    {
        public int Compare(ICard card1, ICard card2)
        {
            int a = Convert.ToInt32(card1.Rank);
            int b = Convert.ToInt32(card2.Rank);

            if (a < b)
            {
                return 1;
            }
            else if (a > b)
            {
                return -1;
            }

            return 0;
        }
    }
    class RankCompare : IComparer<CardRank>
    {
        public int Compare(CardRank rank1, CardRank rank2)
        {
            int a = Convert.ToInt32(rank1);
            int b = Convert.ToInt32(rank2);

            if (a < b)
            {
                return 1;
            }
            else if (a > b)
            {
                return -1;
            }
            return 0;
        }
    }
    class CombinationCompare : IComparer<(int, UInt64)>
    {
        public int Compare((int, UInt64) player1, (int, UInt64) player2)
        {
            UInt64 a = player1.Item2;
            UInt64 b = player2.Item2;

            if (a < b)
            {
                return 1;
            }
            else if (a > b)
            {
                return -1;
            }
            return 0;
        }
    }
}
