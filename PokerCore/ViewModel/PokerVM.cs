using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;
using PokerCore.Model;
using System.Reactive;

namespace PokerCore.ViewModel
{
    public class PokerVM : ReactiveObject, ITableVM
    {
        public PokerVM(PokerM m)
        {
            pokerM = m;
            tryConnect = ReactiveCommand.Create<string, bool>(x => pokerM.TryConnect(x));
            call = ReactiveCommand.Create<Unit, Unit>(x => pokerM.Players[CurPlayer].Call());
        }

        private PokerM pokerM;

        public IGameRules Rules { get => pokerM.Rules; set => throw new NotImplementedException(); }

        public Dictionary<int, ICard> HandCards => throw new NotImplementedException();

        public ICardDeck Deck => throw new NotImplementedException();

        public Dictionary<int, IPlayerState> Players => pokerM.Players;

        public IEnumerable<ICard> BoardCards => throw new NotImplementedException();

        public int Dealer => pokerM.Dealer;

        public int CurPlayer => pokerM.CurPlayer;

        public int SmallBlind => pokerM.SmallBlind;

        public int BigBlind => pokerM.BigBlind;

        public int CurrentRaise => pokerM.CurrentRaise;

        public int CurrentBet => pokerM.CurrentBet;

        public int Bank => pokerM.Bank;

        public int Bank2 => pokerM.Bank2;

        ReactiveCommand<string, bool> tryConnect;
        public ReactiveCommand<string, bool> TryConnect { get => tryConnect; }

        ReactiveCommand<Unit, Unit> call;
        public ReactiveCommand<Unit, Unit> Call => call;

        public ReactiveCommand<Unit, Unit> Fold => throw new NotImplementedException();

        public ReactiveCommand<Unit, Unit> Raise => throw new NotImplementedException();

        public ReactiveCommand<Unit, Unit> Check => throw new NotImplementedException();

        public ReactiveCommand<Unit, Unit> AllIn => throw new NotImplementedException();

        public IEnumerable<int> GetStrongestCombination()
        {
            throw new NotImplementedException();
        }
    }
}
