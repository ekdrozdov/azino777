using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;
using PokerCore.Model;
using System.Reactive;

namespace PokerCore.ViewModel
{
    public class PlayerVM : ReactiveObject
    {
        Player _playerM;
        public PlayerVM(Player m)
        {
            _playerM = m;
           // call = ReactiveCommand.Create<Unit, Unit>(x => pokerM.Players[CurPlayer].Call());
        }
        
        ReactiveCommand<Unit, Unit> addCash;
        ReactiveCommand<Unit, Unit> bet;
        ReactiveCommand<Unit, Unit> check;
        ReactiveCommand<Unit, Unit> fold;
        ReactiveCommand<Unit, Unit> call;
        ReactiveCommand<Unit, Unit> raise;
        ReactiveCommand<Unit, Unit> allin;

        public ReactiveCommand<Unit, Unit> Call => call;
        public ReactiveCommand<Unit, Unit> Fold => throw new NotImplementedException();
        public ReactiveCommand<Unit, Unit> Raise => throw new NotImplementedException();
        public ReactiveCommand<Unit, Unit> Check => throw new NotImplementedException();
        public ReactiveCommand<Unit, Unit> AddCash => throw new NotImplementedException();
        public ReactiveCommand<Unit, Unit> Bet => throw new NotImplementedException();
        public ReactiveCommand<Unit, Unit> AllIn => throw new NotImplementedException();

    }
}
