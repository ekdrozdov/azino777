using System;
using System.Collections.Generic;
using System.Text;

namespace PokerCore.Model.DataBase
{
    public class DBCard
    {
        public int Id { get; set; }
        public CardRank Rank { get; set; }
        public CardSuit Suit { get; set; }
    }
}
