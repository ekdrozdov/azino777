using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;

namespace PokerCore.Model
{
    public static class PokerM
    {
        static List<Player> players = new List<Player>(2);
        static GameRules gameRules = new GameRules(10);
        static TableStateForPlayer tableStateForPlayer = new TableStateForPlayer();

        public static void Initialization()
        {
            players.Add(new Player("Илья"));
        }

        public static ITableForPlayer TryConnect(string name)
        {
            if (gameRules.MaxPlayers < players.Count)
            {
                players.Add(new Player(name));
                return new TableForPlayer();
            }
            return new TableForPlayer();
        }

        public static IGameRules GetRules()
        {
            return gameRules;
        }

        public static ITableStateForPlayer GetTableStateForPlayer()
        {
            return tableStateForPlayer;
        }
    }


}
