using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;

namespace PokerCore.Model
{
    public class PokerM
    {
        List<Player> players;
        GameRules gameRules;
        TableStateForPlayer tableStateForPlayer;

        public PokerM(string name, int maxplayer)
        {
            tableStateForPlayer = new TableStateForPlayer();
            gameRules = new GameRules(maxplayer);
            players = new List<Player>(maxplayer);
            players.Add(new Player(name));
        }

        public ITableForPlayer TryConnect(string name)
        {
            if (gameRules.MaxPlayers < players.Count)
            {
                players.Add(new Player(name));
                return new TableForPlayer();
            }
            return new TableForPlayer();
        }

        public IGameRules GetRules()
        {
            return gameRules;
        }

        public ITableStateForPlayer GetTableStateForPlayer()
        {
            return tableStateForPlayer;
        }
    }


}
