﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Reactive;

namespace PokerCore.Model
{
    public class Player
    {
        PlayerState _myState;
        public PlayerState MyState { get; set; }

        (Card, Card) _handCards;
        public (Card, Card) HandCards { get => _handCards; }

        TableForPlayer _table;

        public Player(string name, int cash)
        {
            //Name = name;
        }

        public void AddCash(int cash)
        {
            throw new NotImplementedException();
        }

        public void Fold()
        {
            throw new NotImplementedException();
        }

        public Unit Call()
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
