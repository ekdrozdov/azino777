using System;
using System.Collections.Generic;
using System.Text;

namespace PokerCore.Model
{
    public class CardDeck
    {
        public CardDeck()
        {

        }

        private List<Card> _restCards;

        public List<ICard> RestCards { get; }

        public ICard TakeCard()
        {
            Card Taked = _restCards[_restCards.Count - 1];
            _restCards.RemoveAt(_restCards.Count - 1);
            return Taked;
        }

        public void Shuffle()
        {

            //_restCards = _restCards.Sort();
        }
    }
}
