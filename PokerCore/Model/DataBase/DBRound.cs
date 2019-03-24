using System;
using System.Collections.Generic;
using System.Text;

namespace PokerCore.Model.DataBase
{
    public class DBRound
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Game { get; set; }
        public int Player { get; set; }

        public string ActionName { get; set; }
        public int BetSize { get; set; }
        public DateTime DecisionTime { get; set; }
    }
}
