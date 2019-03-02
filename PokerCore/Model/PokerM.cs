using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;

namespace PokerCore.Model
{
    public class PokerM : ITableStateForPlayer
    {
        List<Player> players;
        GameRules gameRules;
        TableStateForPlayer tableStateForPlayer;

        public IEnumerable<IPlayerState> Players { get => players; }

        public IEnumerable<ICard> BoardCards => throw new NotImplementedException();
        int dealer;
        public int Dealer { get => dealer; }
        int currentRaise;
        public int CurrentRaise { get => currentRaise; }
        int bet;
        public int Bet { get => bet; }
        int bank;
        public int Bank { get => bank; }
        int bank2;
        public int Bank2 { get => bank2; }

        public PokerM(string name, int maxplayer, int startbank)
        {
            bank = startbank;
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
