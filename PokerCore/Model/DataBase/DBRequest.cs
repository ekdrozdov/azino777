using System;
using System.Collections.Generic;
using System.Text;

namespace PokerCore.Model.DataBase
{
    class DBRequest
    {
        public List<DBCard> DBHandCards { get; set; }
        public List<DBCard> DBTableCards { get; set; }
        public int LastBet { get; set; }
        public int StartCash { get; set; }

        public DBRequest()
        {
            List<DBCard> DBHandCards = new List<DBCard>();
            List<DBCard> DBTableCards = new List<DBCard>();
        }
    }
}
