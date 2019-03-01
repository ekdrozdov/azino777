using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;
using PokerCore.Model;

namespace PokerCore.ViewModel
{
    public class PokerVM : ReactiveObject, ITable
    {
        public ITableStateForPlayer TableState { get => PokerM.GetTableStateForPlayer(); set => throw new NotImplementedException(); }
        public IGameRules Rules { get => PokerM.GetRules(); set => throw new NotImplementedException(); }

        public ReactiveCommand<string, ITableForPlayer> TryConnect => 
            ReactiveCommand.Create<string, ITableForPlayer>(x => PokerM.TryConnect(x));
    }
}
