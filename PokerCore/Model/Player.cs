using System;
using System.Collections.Generic;
using System.Text;

namespace PokerCore.Model
{
    class Player : IPlayerState
    {
        public string Name { get; set; }

        public int Cash { get; set; }

        public int PlayerBet { get; set; }

        public int ChairNumber { get; set; }

        public PlayerGameState State { get; set; }

        public ITableBase TableState => throw new NotImplementedException();

        public IEnumerable<ICard> HandCards => throw new NotImplementedException();

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

        public Player(string name)
        {
            Name = name;
        }

        public void SetName()
        {
            throw new NotImplementedException();
        }

        public void AddCash(int cash)
        {
            throw new NotImplementedException();
        }

        public void Fold()
        {
            throw new NotImplementedException();
        }

        public void Call()
        {
            throw new NotImplementedException();
        }

        public void Check()
        {
            throw new NotImplementedException();
        }

        public void Raise(int raise)
        {
            throw new NotImplementedException();
        }

        public void AllIn()
        {
            throw new NotImplementedException();
        }
    }
}
