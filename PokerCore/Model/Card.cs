using System;
using System.Collections.Generic;
using System.Text;

namespace PokerCore.Model
{
    public class Card : ICard
    {
        public Card(CardRank rank, CardSuit suit) { _rank = rank; _suit = suit; }

        private CardRank _rank;
        public CardRank Rank { get => _rank; }

        private CardSuit _suit;
        public CardSuit Suit { get => _suit; }

        public string GetTextureName()
        {
            string name;
            name = "card";

            foreach (string suits in Enum.GetNames(typeof(CardSuit)))
                if (suits.Equals(_suit.ToString()))
                {
                    name += suits;
                    foreach (string ranks in Enum.GetNames(typeof(CardRank)))
                        if (ranks.Equals(_rank.ToString()))
                        {
                            name += ranks;
                            break;
                        }
                }

            name += ".png";
            return "Resources/" + name;
        }

        public static bool operator ==(Card x, Card y)
        {
            return (x.Rank == y.Rank) && (x.Suit == y.Suit);
        }

        public static bool operator !=(Card x, Card y)
        {
            return !(x == y);
        }


    }


}
