using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;
using System.Reactive;

namespace PokerCore
{
    public enum CardSuit { Spades, Dimonds, Hearts, Clubs };
    public enum CardRank { c2, c3, c4, c5, c6, c7, c8, c9, c10, J, Q, K, A };
    public enum PlayerGameState { In, Out, AllIn }
    public interface ICard
    {
        CardRank Rank { get; }
        CardSuit Suit { get; }
    }

    public interface ICardDeck
    {
        List<ICard> CardDeck { get; }

        void Shuffle();
        ICard TakeCard();
    }

    public interface IPlayerState
    {
        string Name { get; }
        int Cash { get; }
        int PlayerBet { get; } // Вы уже вложили в банк за текущий раунд
        PlayerGameState State { get; }
    }

    public interface ITableBase
    {
        IEnumerable<int, IPlayerState> Players { get; }
        IEnumerable<ICard> BoardCards { get; }
        int Dealer { get; }
        int CurPlayer { get; }
        int SmallBlind { get; }
        int BigBlind { get; }
        int CurrentRaise { get; }
        int CurrentBet { get; }
        int Bank { get; }
        int Bank2 { get; } //дополнительный банк, которые нужен после allin
    }

    public interface ITableReal : ITableBase
    {
        IEnumerable<int, ICard> HandCards { get; }
        ICardDeck Deck { get; }
    }

    public interface IVisibility
    {
        public enum Visibility { Visible, Invisible }
        private Visibility CurrentVisibility;
    }

    public interface ITableForPlayer : IPlayerState
    {
        ITableBase TableState { get; }
        IEnumerable<ICard> HandCards { get; }

        ReactiveCommand<string, Unit> SetName { get; }
        ReactiveCommand<int, Unit> AddMoney { get; }
        ReactiveCommand<Unit, Unit> Fold { get; }
        ReactiveCommand<Unit, Unit> Call { get; }
        ReactiveCommand<Unit, Unit> Check { get; }
        ReactiveCommand<int, Unit> Raise { get; }
        ReactiveCommand<Unit, Unit> AllIn { get; }
    }
    public interface IGameRules
    {
        string RulesHelp { get; }
        int MaxPlayers { get; }
    }
    public interface ITable
    {
        ITableReal TableState { get; }
        IGameRules Rules { get; }

        ReactiveCommand<string, ITableForPlayer> TryConnect { get; }
    }
}
