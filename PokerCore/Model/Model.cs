using System;
using System.Collections.Generic;
using System.Text;

namespace PokerCore.Model
{
    public class Card: ICard
    {
        CardRank Rank { get; }
        CardSuit Suit { get; }
        CardVisibility Visibility { get; }
    }
    public class DeckofCards: IDeckOfCards
    {
        List<Card> DeckOfCards { get; }

        ReactiveCommand<Unit, Unit> Reset { get; } //перемешать карты
        ReactiveCommand<Card, Unit> TakeCard { get; }
    }
    public class PlayerState: IPlayerState
    {
        string Name { get; }
        int Cash { get; }
        int PlayerBet { get; } // Вы уже вложили в банк за текущий раунд
        int ChairNumber { get; }
        PlayerGameState State { get; }
    }
    public class TableStateForPlayer: ITableStateForPlayer
    {
        IEnumerable<PlayerState> Players { get; }
        IEnumerable<Card> BoardCards { get; } //карты на столе
        int Dealer { get; }
        int CurrentRaise { get; }
        int Bet { get; }
        int Bank { get; }
        int Bank2 { get; } //дополнительный банк, которые нужен после allin
    }
    public class TableForPlayer: ITableForPlayer
    {
        TableStateForPlayer TableState { get; }
        IEnumerable<Card> HandCards { get; }

        ReactiveCommand<string, Unit> SetName { get; }
        ReactiveCommand<int, Unit> AddMoney { get; }
        ReactiveCommand<Unit, Unit> Fold { get; }
        ReactiveCommand<Unit, Unit> Call { get; }
        ReactiveCommand<Unit, Unit> Check { get; }
        ReactiveCommand<int, Unit> Raise { get; }
        ReactiveCommand<Unit, Unit> AllIn { get; }
    }
    public class GameRules: IGameRules
    {
        string RulesHelp { get; }
        int SmallBlind { get; }
        int BigBlind { get; }
        int MaxPlayers { get; }
    }
    public class Table: ITable
    {
        TableStateForPlayer TableState { get; }
        GameRules Rules { get; }

        ReactiveCommand<string, TableForPlayer> TryConnect { get; }
    }
}
