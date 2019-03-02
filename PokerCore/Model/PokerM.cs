using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;

namespace PokerCore.Model
{
    public class PokerM : ITableReal
    {
        Dictionary<int, Player> players;
        GameRules gameRules;

        public PokerM(string name, int maxplayer, int startbank)
        {
            bank = startbank;
            gameRules = new GameRules(maxplayer);
            players = new Dictionary<int, Player>(maxplayer);
            players[0] = new Player(name);
        }

        public Dictionary<int, ICard> HandCards => throw new NotImplementedException();

        public ICardDeck Deck => throw new NotImplementedException();

        public IGameRules Rules { get => gameRules; }

        public Dictionary<int, IPlayerState> Players => throw new NotImplementedException();

        public IEnumerable<ICard> BoardCards => throw new NotImplementedException();

        public int Dealer => throw new NotImplementedException();

        public int CurPlayer => throw new NotImplementedException();

        public int SmallBlind => throw new NotImplementedException();

        public int BigBlind => throw new NotImplementedException();

        public int CurrentRaise => throw new NotImplementedException();

        public int CurrentBet => throw new NotImplementedException();
        int bank;
        public int Bank { get => bank; }

        public int Bank2 => throw new NotImplementedException();


        public IEnumerable<int> GetStrongestCombination()
        {
            throw new NotImplementedException();
        }

        public bool TryConnect(string name)
        {
            if (gameRules.MaxPlayers < players.Count)
            {
                players.Add(players.Count, new Player(name));
                return true;
            }
            return false;
        }

    }
}
