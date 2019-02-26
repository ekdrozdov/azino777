using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using ReactiveUI;

namespace PokerCore.Model
{
    public class Card: ICard
    {
        CardRank Rank { get; }

        CardRank ICard.Rank => throw new NotImplementedException();

        CardSuit Suit { get; }

        CardSuit ICard.Suit => throw new NotImplementedException();

        CardVisibility Visibility { get; }

        CardVisibility ICard.Visibility => throw new NotImplementedException();
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
        string RulesHelp { get; }

        string IGameRules.RulesHelp => throw new NotImplementedException();

        int SmallBlind { get; }

        int IGameRules.SmallBlind => throw new NotImplementedException();

        int BigBlind { get; }

        int IGameRules.BigBlind => throw new NotImplementedException();

        int MaxPlayers { get; }

        int IGameRules.MaxPlayers => throw new NotImplementedException();
    }
    public class Table: ITable
    {
        TableStateForPlayer TableState { get; }

        ITableStateForPlayer ITable.TableState => throw new NotImplementedException();

        GameRules Rules { get; }

        IGameRules ITable.Rules => throw new NotImplementedException();

        ReactiveCommand<string, TableForPlayer> TryConnect { get; }

        ReactiveCommand<string, ITableForPlayer> ITable.TryConnect => throw new NotImplementedException();
    }
}
