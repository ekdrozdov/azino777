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

    public interface IPlayerState : ITableForPlayer
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

        void TryConnect(string name);
    }

    public interface IVisibility
    {
        Visibility CurrentVisibility { get; }
    }

    public interface ITableForPlayer : ITableBase
    {
        ITableBase TableState { get; }
        IEnumerable<ICard> HandCards { get; }

        void SetName();
        void AddCash(int cash);
        void Fold();
        void Call();
        void Check();
        void Raise(int raise);
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
