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

        public int Dealer { get => _dealer; }

        public int CurPlayer { get => _curPlayer; }

        public int SmallBlind { get => _smallBlind; }

        public int BigBlind { get => _bigBlind; }

        public int CurrentRaise { get => _curRaise; }

        public int CurrentBet { get => _curBet; }

        public int AllBank { get => _allBank; }

        public List<(int, int)> DividedBanks { get => _dividedBanks; }

        public CardDeck Deck { get => _cardDeck; }

        public GameRules Rules { get => _gameRules; }

        public Dictionary<int, Player> Players { get => _players; }

        public List<Card> BoardCards { get => _boardCards; }

        List<(int, (Card, Card))> HandCards;

        public IEnumerable<int> GetStrongestCombination()
        {
            throw new NotImplementedException();
        }

        public void AddBank(int bankBeforeBet)
        {
            _dividedBanks.Add((_curPlayer, bankBeforeBet));
        }

        public void BankDivision()
        {

        }

        public bool EndAction()
        {
            return false;
        }

        public bool TryConnect(string name, int cash)
        {
            if (_players.Count  < _gameRules.MaxPlayers)
            {
                _players.Add(_players.Count, new Player(name, cash));
                return true;
            }
            return false;
        }

        public void Disconnect(int key)
        {
            _players.Remove(key);
        }

        public string GetTextureName(CardRank rank, CardSuit suit)
        {
            string name;
            name = "card";

            foreach (string suits in Enum.GetNames(typeof(CardSuit)))
            {
                if (suits.Equals(suit.ToString()))
                {
                    name += suits;
                    foreach (string ranks in Enum.GetNames(typeof(CardRank)))
                    {
                        if (ranks.Equals(suit.ToString()))
                        {
                            name += ranks;
                        }
                    }
                }
            }

            name += ".png";
            return name;
        }
    }
}