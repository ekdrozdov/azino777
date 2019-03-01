using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using ReactiveUI;

namespace PokerCore.Model
{
    public class Card: ICard
    {
        public CRank CSuit Card => 
        CardRank _rank;
        public CardRank Rank { get => _rank; }

        CardSuit _suit;
        public CardSuit Suit { get => _suit; }
    }
    public class DeckofCards: IDeckOfCards
    {
        List<Card> DeckOfCards { get; }

        List<ICard> IDeckOfCards.DeckOfCards => throw new NotImplementedException();

        ReactiveCommand<Unit, Unit> Reset { get; } //перемешать карты

        ReactiveCommand<Unit, Unit> IDeckOfCards.Reset => throw new NotImplementedException();

        ReactiveCommand<Card, Unit> TakeCard { get; }

        ReactiveCommand<ICard, Unit> IDeckOfCards.TakeCard => throw new NotImplementedException();
    }
    public class PlayerState: IPlayerState
    {
        string Name { get; }

        string IPlayerState.Name => throw new NotImplementedException();

        int Cash { get; }

        int IPlayerState.Cash => throw new NotImplementedException();

        int PlayerBet { get; } // Вы уже вложили в банк за текущий раунд

        int IPlayerState.PlayerBet => throw new NotImplementedException();

        int ChairNumber { get; }

        int IPlayerState.ChairNumber => throw new NotImplementedException();

        PlayerGameState State { get; }

        PlayerGameState IPlayerState.State => throw new NotImplementedException();
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
