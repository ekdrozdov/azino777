using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;
using System.Reactive;

//DEPRECATED
namespace PokerCore
{
    public enum CardSuit { Spades, Dimonds, Hearts, Clubs };
    public enum CardRank
    {
        c2  = 2,
        c3  = 3,
        c4  = 4,
        c5  = 5,
        c6  = 6,
        c7  = 7,
        c8  = 8,
        c9  = 9,
        c10 = 10,
        J   = 11,
        Q   = 12,
        K   = 13,
        A   = 14
    };
    public enum PlayerGameState { In, Out, AllIn }
    public enum Visibility { Visible, Invisible }
    public interface ICard
    {
        CardRank Rank { get; }
        CardSuit Suit { get; }
    }

    public interface ICardDeck
    {
        List<ICard> RestCards { get; }

        void Shuffle();
        ICard TakeCard();
    }

    public interface IPlayerState : IPlayer
    {
        string Name { get; }
        int Cash { get; }
        int PlayerBet { get; } // Вы уже вложили в банк за текущий раунд
        PlayerGameState State { get; }
    }

    public interface ITableBase
    {
        Dictionary<int, IPlayerState> Players { get; }
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
        Dictionary<int, ICard> HandCards { get; }
        ICardDeck Deck { get; }

        IEnumerable<int> GetStrongestCombination();
        IGameRules Rules { get; }

    }

    public interface IVisibility
    {
        Visibility CurrentVisibility { get; }
    }

    public interface ITableVM : ITableReal, ITableForPlayer
    {
        ReactiveCommand<string, bool> TryConnect { get; }
    }

    public interface ITableForPlayer
    {
        ReactiveCommand<Unit, Unit> Call { get; }
        ReactiveCommand<Unit, Unit> Fold { get; }
        ReactiveCommand<Unit, Unit> Raise { get; }
        ReactiveCommand<Unit, Unit> Check { get; }
        ReactiveCommand<Unit, Unit> AllIn { get; }
    }

    public interface IPlayer : ITableBase
    {
        ITableBase TableState { get; }
        IEnumerable<ICard> HandCards { get; }

        void Call();
        void Fold();
        void Raise(int raise);
        void Check();
        void AllIn();
    }

    public interface IGameRules
    {
        string RulesHelp { get; }
        int MaxPlayers { get; }
    }

    public interface ITable
    {
        ITableReal TableState { get; }
        
    }
}
