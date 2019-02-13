using System;
using System.Collections.Generic;
using ReactiveUI;
using System.Reactive;

namespace PokerCore
{
    public enum PlayerGameState { In, Out, AllIn }
    public interface IPlayerState
    {
        string Name { get; }
        int Cash { get; }
        int PlayerBet { get; } // Вы уже вложили в банк за текущий раунд
        int ChairNumber { get; }
        PlayerGameState State { get; }
    }
    public interface ICard { }
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
}
