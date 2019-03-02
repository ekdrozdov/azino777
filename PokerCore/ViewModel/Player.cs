using System;
using System.Collections.Generic;
using System.Reactive;
using ReactiveUI;

namespace PokerCore.ViewModel
{
    public class Player : ReactiveObject, ITableForPlayer
    {
        public ReactiveCommand<string, Unit> SetName => throw new NotImplementedException();

        public ReactiveCommand<int, Unit> AddMoney => throw new NotImplementedException();

        public ReactiveCommand<Unit, Unit> Fold => throw new NotImplementedException();

        public ReactiveCommand<int, Unit> Bet => throw new NotImplementedException();

        public ReactiveCommand<Unit, Unit> Call => throw new NotImplementedException();

        public ReactiveCommand<Unit, Unit> Check => throw new NotImplementedException();

        public ReactiveCommand<int, Unit> Raise => throw new NotImplementedException();

        public ReactiveCommand<Unit, Unit> AllIn => throw new NotImplementedException();

        public IEnumerable<ICard> Cards => throw new NotImplementedException();

        public ITableStateForPlayer TableState => throw new NotImplementedException();

        private string _name;
        public string Name  { get => _name; set => _name = value; }
        public int Cash => throw new NotImplementedException();

        public int PlayerBet => throw new NotImplementedException();

        public int ChairNumber => throw new NotImplementedException();

        public PlayerGameState State => throw new NotImplementedException();

        public IEnumerable<ICard> HandCards => throw new NotImplementedException();
    }
}
