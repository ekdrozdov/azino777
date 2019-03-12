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

        public string GetTextureName(CardRank rank, CardSuit suit)
        {
            string name;
            name = "card";

            foreach (string suits in Enum.GetNames(typeof(CardSuit)))
                if (suits.Equals(suit.ToString()))
                {
                    name += suits;
                    foreach (string ranks in Enum.GetNames(typeof(CardRank)))
                        if (ranks.Equals(suit.ToString()))
                            name += ranks;
                }

            name += ".png";
            return name;
        }
    }
}
