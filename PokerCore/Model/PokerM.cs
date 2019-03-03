using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;

namespace PokerCore.Model
{
    public class PokerM
    {
        Dictionary<int, Player> _players;
        GameRules _gameRules;
        List<Card> _boardCards;
        CardDeck _cardDeck;
        int _curPlayer;
        int _dealer;
        int _smallBlind;
        int _bigBlind;
        int _curBet;
        int _curRaise;
        int _allBank;
        List<(int, int)> _dividedBanks;

        public PokerM(string name, int maxplayer, int startbank)
        {
            _allBank = startbank;
            //gameRules = new GameRules(maxplayer);
            //players = new Dictionary<int, Player>(maxplayer);
            //players[0] = new Player(name);
        }

        public int Dealer => throw new NotImplementedException();

        public int CurPlayer => throw new NotImplementedException();

        public int SmallBlind => throw new NotImplementedException();

        public int BigBlind => throw new NotImplementedException();

        public int CurrentRaise => throw new NotImplementedException();

        public int CurrentBet => throw new NotImplementedException();

        public int AllBank { get => _allBank; }

        public List<(int, int)> DividedBanks { get; }

        public ICardDeck Deck => throw new NotImplementedException();

        public GameRules Rules { get => _gameRules; }

        public Dictionary<int, PlayerState> Players => throw new NotImplementedException();

        public List<ICard> BoardCards => throw new NotImplementedException();

        List<(int, (ICard, ICard))> HandCards;

        public IEnumerable<int> GetStrongestCombination()
        {
            throw new NotImplementedException();
        }

        public void AddBank()
        {

        }

        public void BankDivision()
        {

        }

        public bool EndAction()
        {
            return false;
        }

        public bool TryConnect(string name)
        {
            //if (gameRules.MaxPlayers < players.Count)
            //{
            //    players.Add(players.Count, new Player(name));
            //    return true;
            //}
            return false;
        }

        public void Disconnect(int key)
        {

        }
    }
}
