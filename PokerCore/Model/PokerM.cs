using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;

namespace PokerCore.Model
{
    public static class PokerM
    {
        static List<Player> players = new List<Player>();
        static GameRules gameRules = new GameRules(10);
        static TableStateForPlayer tableStateForPlayer = new TableStateForPlayer();

        public static ReactiveCommand<string, ITableForPlayer> TryConnect(string name, int cash)
        {
            if (gameRules.MaxPlayers < players.Count)
            {
                players.Add(new Player());
                return ReactiveCommand.Create<string, ITableForPlayer>(x => new TableForPlayer());
            }
            return ReactiveCommand.Create<string, ITableForPlayer>(x => new TableForPlayer());
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
