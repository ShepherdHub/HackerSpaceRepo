using System;
using System.Collections.Generic;

namespace GameOfWarOOP
{
    class Deck
    {
        private List<Card> _cards = new List<Card>();
        private Random randNum = new Random();

        public Deck()
        {
        }

        public Deck(int size)
        {
            for (int i = 0; i < size; i++)
            {
                AddCard(i);
            }
        }

        public void AddCard(Card card)
        {
            _cards.Add(card);
        }

        public void AddCard(int i)
        {
            Card card = new Card(i);
            AddCard(card);
        }

        public Card RemoveCard()
        {
            if(_cards.Count<=0)
            {
                Console.WriteLine("Out of Bounds Exception: Tried to remove a card from an empty deck");
                return null;
            }

            Card card = _cards[0];
            _cards.RemoveAt(0);

            return card;
        }

        public Card RemoveCard(int index)
        {
            if (_cards.Count <= 0)
            {
                Console.WriteLine("Out of Bounds Exception: Tried to remove a card from an empty deck");
                return null;
            }

            Card card = _cards[index];
            _cards.RemoveAt(index);

            return card;
        }

        public void Shuffle()
        {
            for (int i = 0; i < _cards.Count; i++)
            {
                Swap(i, randNum.Next(i, _cards.Count));
            }
        }

        private void Swap(int a, int b)
        {
            Card temp = _cards[a];
            _cards[a] = _cards[b];
            _cards[b] = temp;
        }
    }
}
