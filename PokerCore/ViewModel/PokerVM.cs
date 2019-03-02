using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;
using PokerCore.Model;

namespace PokerCore.ViewModel
{
    public class PokerVM : ReactiveObject, ITableReal
    {
        public PokerVM(PokerM m)
        {
            pokerM = m;
            TryConnect = ReactiveCommand.Create<string, bool>(x => pokerM.TryConnect(x));
        }

        private PokerM pokerM;
        public IGameRules Rules { get => pokerM.Rules; set => throw new NotImplementedException(); }

        public ReactiveCommand<string, bool> TryConnect { get; }

        public Dictionary<int, ICard> HandCards => throw new NotImplementedException();

        public ICardDeck Deck => throw new NotImplementedException();

        public Dictionary<int, IPlayerState> Players => throw new NotImplementedException();

        public IEnumerable<ICard> BoardCards => throw new NotImplementedException();

        public int Dealer => throw new NotImplementedException();

        public int CurPlayer => throw new NotImplementedException();

        public int SmallBlind => throw new NotImplementedException();

        public int BigBlind => throw new NotImplementedException();

        public int CurrentRaise => throw new NotImplementedException();

        public int CurrentBet => throw new NotImplementedException();

        public int Bank => throw new NotImplementedException();

        public int Bank2 => throw new NotImplementedException();

        public IEnumerable<int> GetStrongestCombination()
        {
            throw new NotImplementedException();
        }
    }
}
