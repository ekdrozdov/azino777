using System;
using System.Collections.Generic;
using System.Text;

namespace PokerCore.Model
{
    public class Card : ICard
    {
        public Card(CardRank rank, CardSuit suit) { _rank = rank; _suit = suit; }

        CardRank _rank;
        public CardRank Rank { get => _rank; }

        CardSuit _suit;
        public CardSuit Suit { get => _suit; }
    }
}
