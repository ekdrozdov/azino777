using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using ReactiveUI;

namespace PokerCore.Model
{
    public class Card : ICard
    {
        public Card( CardRank rank, CardSuit suit) { _rank = rank; _suit = suit; }

        CardRank _rank;
        public CardRank Rank { get => _rank; }

        CardSuit _suit;
        public CardSuit Suit { get => _suit; }
    }

    public class CardDeck: ICardDeck
    {
        public CardDeck()
        {

        }

        private List<Card> _restCards;

        public List<ICard> RestCards { get; }

        public ICard TakeCard()
        {
            Card Taked = _restCards[_restCards.Count - 1];
            _restCards.RemoveAt(_restCards.Count - 1);
            return Taked;
        }

        public void Shuffle() { }
    }

    public class PlayerState: IPlayerState
    {
        public PlayerState()
        {

        }

        string _name;
        public string Name { get => _name; }

        int _cash;
        public int Cash { get => _cash; set { _cash = value; } }

        int _playerBet;
        public int PlayerBet { get => _playerBet; set { _playerBet = value; } }

        PlayerGameState _state;
        public PlayerGameState State { get => _state; set { _state = value; } }
    }

    public class TableBase: ITableBase
    {
        Dictionary<int, PlayerState> _players;
        public Dictionary<int, IPlayerState> Players { get; }

        List<Card> _boardCards;
        public IEnumerable<ICard> BoardCards { get => _boardCards; }

        int _dealer;
        public int Dealer { get => _dealer; }

        int _curPlayer;
        public int CurPlayer { get => _curPlayer; }

        int _smallBlind;
        public int SmallBlind { get => _smallBlind; }

        int _bigBlind;
        public int BigBlind { get => _bigBlind; }

        int _currentRaise;
        public int CurrentRaise { get => _currentRaise; }

        int _currentBet;
        public int CurrentBet { get => _currentBet; }

        int _bank;
        public int Bank { get => _bank; }

        int _bank2;
        public int Bank2 { get => _bank2; }
    }

    public class TableForPlayer : ITableForPlayer
    {
        public TableForPlayer()
        {
            
        }

        string _name;
        public string Name { get => _name; }

        int _cash;
        public int Cash { get => _cash; set { _cash = value; } }

        int _playerBet;
        public int PlayerBet { get => _playerBet; set { _playerBet = value; } }

        PlayerGameState _state;
        public PlayerGameState State { get => _state; set { _state = value; } }

        Dictionary<int, PlayerState> _players;
        public Dictionary<int, IPlayerState> Players { get; }

        List<Card> _boardCards;
        public IEnumerable<ICard> BoardCards { get => _boardCards; }

        int _dealer;
        public int Dealer { get => _dealer; }

        int _curPlayer;
        public int CurPlayer { get => _curPlayer; }

        int _smallBlind;
        public int SmallBlind { get => _smallBlind; }

        int _bigBlind;
        public int BigBlind { get => _bigBlind; }

        int _currentRaise;
        public int CurrentRaise { get => _currentRaise; }

        int _currentBet;
        public int CurrentBet { get => _currentBet; }

        int _bank;
        public int Bank { get => _bank; }

        int _bank2;
        public int Bank2 { get => _bank2; }

        TableBase _tableState;
        public ITableBase TableState { get; }

        List<Card> _handCards;
        public IEnumerable<ICard> HandCards { get; }

        public void SetName()
        { }
        public void AddCash(int cash)
        { }
        public void Fold()
        { }
        public void Call()
        { }
        public void Check()
        {

        }
        public void Raise(int raise)
        { }
        public void AllIn()
        { }

    }

    public class TableReal : ITableReal
    {
        Dictionary<int, PlayerState> _players;
        public Dictionary<int, IPlayerState> Players { get; }

        List<Card> _boardCards;
        public IEnumerable<ICard> BoardCards { get => _boardCards; }

        int _dealer;
        public int Dealer { get => _dealer; }

        int _curPlayer;
        public int CurPlayer { get => _curPlayer; }

        int _smallBlind;
        public int SmallBlind { get => _smallBlind; }

        int _bigBlind;
        public int BigBlind { get => _bigBlind; }

        int _currentRaise;
        public int CurrentRaise { get => _currentRaise; }

        int _currentBet;
        public int CurrentBet { get => _currentBet; }

        int _bank;
        public int Bank { get => _bank; }

        int _bank2;
        public int Bank2 { get => _bank2; }

        CardDeck _deck;
        public ICardDeck Deck { get; }

        public IEnumerable<int> GetStrongestCombination()
        {
            List<int> smth = new List<int>() { 1, 2 };
            return smth;
        }

        Dictionary<int, Card> _handCards;
        public Dictionary<int, ICard> HandCards { get; }

    }
    
    public class GameRules: IGameRules
    {
        public GameRules(int maxPlayers)
        {
            _maxPlayers = maxPlayers;
        }
        string _rulesHelp => System.IO.File.ReadAllText(@"PokerCore\\resources\\Rules.txt", Encoding.Default).Replace("\n", " ");
        public string RulesHelp { get => _rulesHelp; }

        int _maxPlayers;
        public int MaxPlayers { get => _maxPlayers; }
    }
}
