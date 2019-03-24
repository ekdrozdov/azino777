using System;
using System.Collections.Generic;
using System.Text;

namespace PokerCore.Model.DataBase
{
    public class DBGame
    {
        public int Id { get; set; }

        public DBTableCards TableCards { get; set; }
    }
}
