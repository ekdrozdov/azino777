using System;
using System.Collections.Generic;
using System.Text;

namespace PokerCore.Model.DataBase
{
    public class DBTableCards
    {
        public int Id { get; set; }

        public int DBGameId { get; set; }
        public DBGame Game { get; set; }

        public DBCard FirstCard { get; set; }
        public DBCard SecondCard { get; set; }
        public DBCard ThirdCard { get; set; }
        public DBCard FourthCard { get; set; }
        public DBCard FifthCard { get; set; }
    }
}
