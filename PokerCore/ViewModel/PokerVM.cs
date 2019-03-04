using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;
using PokerCore.Model;
using System.Reactive;

namespace PokerCore.ViewModel
{
    public class PokerVM : ReactiveObject
    {
        public PokerVM(PokerM m)
        {
            pokerM = m;
            //tryConnect = ReactiveCommand.Create<string, bool>(x => pokerM.TryConnect(x));
            //call = ReactiveCommand.Create<Unit, Unit>(x => pokerM.Players[CurPlayer].Call());
        }

        private PokerM pokerM;

        void endAction()
        {

        }

        public GameRules Rules { get => pokerM.Rules; }

        ReactiveCommand<string, Unit> tryConnect;
        public ReactiveCommand<string, Unit> TryConnect { get => tryConnect; }

        ReactiveCommand<int, Unit> disconnect;
        public ReactiveCommand<int, Unit> Disconnect { get => disconnect; }
    }
}
