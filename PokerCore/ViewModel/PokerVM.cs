using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;
using PokerCore.Model;

namespace PokerCore.ViewModel
{
    public class PokerVM : ReactiveObject, ITable
    {
        public PokerVM(PokerM m)
        {
            pokerM = m;
            TryConnect = ReactiveCommand.Create<string, ITableForPlayer>(x => pokerM.TryConnect(x));
        }

        private PokerM pokerM;
        public ITableStateForPlayer TableState { get => pokerM.GetTableStateForPlayer(); set => throw new NotImplementedException(); }
        public IGameRules Rules { get => pokerM.GetRules(); set => throw new NotImplementedException(); }

        public ReactiveCommand<string, ITableForPlayer> TryConnect { get; }
    }
}
