using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;
using System.Reactive;

namespace PokerCore
{
    public enum CardSuit { Spades, Dimonds, Hearts, Clubs };
    public enum CardRank { c2, c3, c4, c5, c6, c7, c8, c9, c10, J, Q, K, A };
    public enum CardVisibility { Visible, Invisible};
    public enum PlayerGameState { In, Out, AllIn }
    public interface ICard
    {
        CardRank Rank { get; }
        CardSuit Suit { get; }
        CardVisibility Visibility { get; }
    }
    public interface IDeckOfCards
    {
        List<ICard> DeckOfCards { get; }

        ReactiveCommand<Unit, Unit> Reset { get; } //перемешать карты
        ReactiveCommand<ICard, Unit> TakeCard { get; }
    }
    public interface IPlayerState
    {
        string Name { get; }
        int Cash { get; }
        int PlayerBet { get; } // Вы уже вложили в банк за текущий раунд
        int ChairNumber { get; }
        PlayerGameState State { get; }
    }
    public interface ITableStateForPlayer
    {
        IEnumerable<IPlayerState> Players { get; }
        IEnumerable<ICard> OpenCards { get; }
        int Dealer { get; }
        int CurrentRaise { get; }
        int Bet { get; }
        int Bank { get; }
        int Bank2 { get; }
    }
    public interface ITableForPlayer : IPlayerState
    {
        ReactiveCommand<string, Unit> SetName { get; }
        ReactiveCommand<int, Unit> AddMoney { get; }
        ReactiveCommand<Unit, Unit> Fold { get; }
        ReactiveCommand<Unit, Unit> Call { get; }
        ReactiveCommand<Unit, Unit> Check { get; }
        ReactiveCommand<int, Unit> Raise { get; }
        ReactiveCommand<Unit, Unit> AllIn { get; }
        IEnumerable<ICard> Cards { get; }

        ITableStateForPlayer TableState { get; }
    }
    public interface IGameRules
    {
        string RulesHelp { get; }
        int SmallBlind { get; }
        int BigBlind { get; }
        int MaxPlayers { get; }
    }
    public interface ITable
    {
        ReactiveCommand<string, ITableForPlayer> TryConnect { get; }
        ITableStateForPlayer State { get; }
        IGameRules Rules { get; }
    }
}
