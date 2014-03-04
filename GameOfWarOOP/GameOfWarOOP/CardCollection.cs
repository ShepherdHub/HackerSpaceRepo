using System;
using System.Collections.Generic;

namespace GameOfWarOOP
{
    class CardCollection
    {
        #region Fields
        private List<PlayingCard> _cards = new List<PlayingCard>();
        private Random _randNum = new Random();
        #endregion

        #region Properties
        /// <summary>
        /// Does not check for whether or not there are cards in the collection.
        /// </summary>
        public PlayingCard TopCard
        {
            get { return _cards[0]; }
        }

        public int Count
        {
            get { return _cards.Count; }
        }
        #endregion

        public CardCollection()
        {
        }

        public CardCollection(int size)
        {
            for (int i = 0; i < size; i++)
            {
                AddCard(i);
            }
        }

        public void AddCard(PlayingCard card)
        {
            _cards.Add(card);
        }

        public void AddCard(int i)
        {
            PlayingCard card = new PlayingCard(i);
            AddCard(card);
        }

        public void AddCardCollection(CardCollection collec)
        {
            _cards.AddRange(collec._cards);
            collec.Clear();
        }

        public PlayingCard RemoveCard()
        {
            if(_cards.Count<=0)
            {
                Console.WriteLine("Out of Bounds Exception: Tried to remove a card from an empty deck");
                return null;
            }

            PlayingCard card = _cards[0];
            _cards.RemoveAt(0);

            return card;
        }

        public PlayingCard RemoveCard(int index)
        {
            if (_cards.Count <= 0)
            {
                Console.WriteLine("Out of Bounds Exception: Tried to remove a card from an empty deck");
                return null;
            }

            PlayingCard card = _cards[index];
            _cards.RemoveAt(index);

            return card;
        }

        public void Shuffle()
        {
            for (int i = 0; i < _cards.Count; i++)
            {
                Swap(i, _randNum.Next(i, _cards.Count));
            }
        }

        public void Clear()
        {
            _cards.Clear();
        }

        public PlayingCard ViewCardAt(int index)
        {
            if(index < _cards.Count && index >= 0) return _cards[index];
            Console.WriteLine("Attempted to view card at invalid index: " + index);
            return null;
        }

        private void Swap(int a, int b)
        {
            PlayingCard temp = _cards[a];
            _cards[a] = _cards[b];
            _cards[b] = temp;
        }
    }
}
