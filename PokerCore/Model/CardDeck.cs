using System;
using System.Collections.Generic;
using System.Text;

namespace PokerCore.Model
{
    public class CardDeck
    {
        public CardDeck()
        {
            _restCards = new List<Card>();

            foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
            {
                foreach (CardRank rank in Enum.GetValues(typeof(CardRank)))
                {
                    _restCards.Add(new Card(rank, suit));
                }
            }
            Shuffle();
        }

        private List<Card> _restCards;

        public List<Card> RestCards { get => _restCards; }

        public ICard TakeCard()
        {
            Card Taken = _restCards[_restCards.Count - 1];
            _restCards.RemoveAt(_restCards.Count - 1);
            return Taken;
        }

        public void Shuffle()
        {
            Card temp;
            Random random = new Random();
            int timesToShuffle = 100;

            int cardToShuffle1, cardToShuffle2;

            for (int i = 0; i < timesToShuffle; i++)
            {
                cardToShuffle1 = random.Next(_restCards.Count);
                cardToShuffle2 = random.Next(_restCards.Count);

                temp = _restCards[cardToShuffle1];
                _restCards[cardToShuffle1] = _restCards[cardToShuffle2];
                _restCards[cardToShuffle2] = temp;
            }
        }
    }
}
