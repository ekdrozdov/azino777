using System;
using System.Collections.Generic;
using System.Text;

namespace PokerCore.Model.DataBase
{
    public class DBPlayerGame
    {
        public int Id { get; set; }

        public int DBPlayerId { get; set; }
        public DBPlayer Player { get; set; }

        public int DBGameId { get; set; }
        public DBGame Game { get; set; }
    }
}
