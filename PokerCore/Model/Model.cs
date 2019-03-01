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
        public int Cash { get => _cash; }

        int _playerBet;
        public int PlayerBet { get => _playerBet; }

        PlayerGameState _state;
        public PlayerGameState State { get => _state; }
    }

    public class TableBase: ITableBase
    {
        Dictionary<int, PlayerState> _players;
        public Dictionary<int, IPlayerState> Players { get; }

        List<Card> _boardCards;
        public IEnumerable<ICard> BoardCards { get; }

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

    public class TableStateForPlayer: ITableStateForPlayer
    {
        IEnumerable<PlayerState> Players { get; }

        IEnumerable<IPlayerState> ITableStateForPlayer.Players => throw new NotImplementedException();

        IEnumerable<Card> BoardCards { get; } //карты на столе

        IEnumerable<ICard> ITableStateForPlayer.BoardCards => throw new NotImplementedException();

        int Dealer { get; }

        int ITableStateForPlayer.Dealer => throw new NotImplementedException();

        int CurrentRaise { get; }

        int ITableStateForPlayer.CurrentRaise => throw new NotImplementedException();

        int Bet { get; }

        int ITableStateForPlayer.Bet => throw new NotImplementedException();

        int Bank { get; }

        int ITableStateForPlayer.Bank => throw new NotImplementedException();

        int Bank2 { get; } //дополнительный банк, которые нужен после allin

        int ITableStateForPlayer.Bank2 => throw new NotImplementedException();
    }
    public class TableForPlayer: ITableForPlayer
    {
        public string Name => throw new NotImplementedException();

        public int Cash => throw new NotImplementedException();

        public int PlayerBet => throw new NotImplementedException();

        public int ChairNumber => throw new NotImplementedException();

        public PlayerGameState State => throw new NotImplementedException();

        TableStateForPlayer TableState { get; }

        ITableStateForPlayer ITableForPlayer.TableState => throw new NotImplementedException();

        IEnumerable<Card> HandCards { get; }

        IEnumerable<ICard> ITableForPlayer.HandCards => throw new NotImplementedException();

        ReactiveCommand<string, Unit> SetName { get; }

        ReactiveCommand<string, Unit> ITableForPlayer.SetName => throw new NotImplementedException();

        ReactiveCommand<int, Unit> AddMoney { get; }

        ReactiveCommand<int, Unit> ITableForPlayer.AddMoney => throw new NotImplementedException();

        ReactiveCommand<Unit, Unit> Fold { get; }

        ReactiveCommand<Unit, Unit> ITableForPlayer.Fold => throw new NotImplementedException();

        ReactiveCommand<Unit, Unit> Call { get; }

        ReactiveCommand<Unit, Unit> ITableForPlayer.Call => throw new NotImplementedException();

        ReactiveCommand<Unit, Unit> Check { get; }

        ReactiveCommand<Unit, Unit> ITableForPlayer.Check => throw new NotImplementedException();

        ReactiveCommand<int, Unit> Raise { get; }

        ReactiveCommand<int, Unit> ITableForPlayer.Raise => throw new NotImplementedException();

        ReactiveCommand<Unit, Unit> AllIn { get; }

        ReactiveCommand<Unit, Unit> ITableForPlayer.AllIn => throw new NotImplementedException();
    }
    public class GameRules: IGameRules
    {
        public GameRules(int maxPlayers)
        {
            _maxPlayers = maxPlayers;
            _rulesHelp = IGameRules.RulesHelp();
        }
        string _rulesHelp;
        string RulesHelp { get => _rulesHelp; }

        string IGameRules.RulesHelp => System.IO.File.ReadAllText(@"PokerCore\\resources\\Rules.txt", Encoding.Default).Replace("\n", " ");

        public string Rules { }
        int _maxPlayers;
        public int MaxPlayers { get => _maxPlayers; }
    }
}
