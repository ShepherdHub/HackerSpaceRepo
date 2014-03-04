using System;
using System.Collections.Generic;

namespace GameOfWarOOP
{
    class Player
    {
        #region Fields
        private string _name;
        private CardCollection _hand = new CardCollection();
        private CardCollection _discard = new CardCollection();
        private CardCollection _spoils = new CardCollection();
        #endregion

        #region Properties
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }

        public CardCollection Hand 
        {
            set { _hand = value; }
            get { return _hand; }
        }

        public CardCollection Discard
        {
            set { _discard = value; }
            get { return _discard; }
        }

        public CardCollection Spoils
        {
            set { _spoils = value; }
            get { return _spoils; }
        }

        public PlayingCard ActiveSpoil
        {
            get 
            {
                int index = _spoils.Count - 1;
                return _spoils.ViewCardAt(index); 
            }
        }

        public int Score
        {
            get { return _hand.Count+_discard.Count+_spoils.Count; }
        }

        public int CardsLeftToDraw
        {
            get { return _hand.Count + _discard.Count; }
        }

        public int PossibleBounty
        {
            get
            {
                if (CardsLeftToDraw < 4) return CardsLeftToDraw - 1;
                return 3;
            }
        }

        #endregion

        public void AddCardToHand(PlayingCard card)
        {
            _hand.AddCard(card);
        }

        public void AddCardToSpoils(PlayingCard card)
        {
            _spoils.AddCard(card);
        }

        public PlayingCard PlayCardFromHand()
        {
            return _hand.RemoveCard();
        }

        public void CollectSpoils(Player losingPlayer)
        {
            _discard.AddCardCollection(_spoils);
            _discard.AddCardCollection(losingPlayer._spoils);
        }

        public void ClearSpoils()
        {
            _spoils.Clear();
        }

        public void RehandIfNecessary(int numCards)
        {
            if (_hand.Count < numCards)
            {
                _discard.Shuffle();
                _hand.AddCardCollection(_discard);
            }
        }
    }
}
