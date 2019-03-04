using System;
using System.Collections.Generic;
using System.Text;

namespace PokerCore.Model.DataBase
{
    public class DBPlayer
    {
        public int Id { get; set; }
        public DBCard FirstCard { get; set; }
        public DBCard SecondCard { get; set; }
        public int StartCash { get; set; } //кэш на начало игры
        public int EndCash { get; set; } //кэш после игры

        public List<DBPlayerGame> PlayersGames { get; set; }
        public DBPlayer()
        {
            PlayersGames = new List<DBPlayerGame>();
        }
    }
}
