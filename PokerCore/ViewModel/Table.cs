using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;

namespace PokerCore.ViewModel
{
    public class PokerTable : ReactiveObject, ITable
    {
        public ITableStateForPlayer TableState => throw new NotImplementedException();

        public IGameRules Rules => throw new NotImplementedException();

        public ReactiveCommand<string, ITableForPlayer> TryConnect => throw new NotImplementedException();
    }
}
