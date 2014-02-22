
namespace GameOfWarOOP
{
    class Card
    {
        #region Fields
        private string _fullName;
        private string _rank;
        private string _suit;
        #endregion

        #region Properties
        public string FullName
        {
            get { return _fullName; }
        }
        public string Rank
        {
            get { return _rank; }
        }
        public string Suit
        {
            get { return _suit; }
        }

        #endregion

        public Card(int card)
        {
            _rank = PrintRank(card);
            _suit = PrintSuit(card);
            _fullName = _rank + " of " + _suit;
        }

        private string PrintRank(int card)
        {
            int rank = card / 13;
            if (rank <= 8)
            {
                return rank +"";
            }
            else
            {
                switch (rank)
                {
                    case (9):
                        return "Jack";
                    case (10):
                        return "Queen";
                    case (11):
                        return "King";
                    case (12):
                        return "Ace";
                    default:
                        return "No rank for card";
                }

            }
        }

        private string PrintSuit(int card)
        {
            int suit = card % 13;
            switch (suit)
            {
                case 0:
                    return "Clubs";
                case 1:
                    return "Diamonds";
                case 2:
                    return "Hearts";
                case 3:
                    return "Spades";
                default:
                    return "No suit found for this card";
            }
        }
    }
}
