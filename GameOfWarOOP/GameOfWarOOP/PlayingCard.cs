
namespace GameOfWarOOP
{
    class PlayingCard
    {
        #region Fields
        private string _fullName;
        private int _rank;
        private int _suit;
        private bool _isFaceUp;
        #endregion

        #region Properties
        public string FullName
        {
            get { return _fullName; }
        }
        public int Rank
        {
            get { return _rank; }
        }
        public int Suit
        {
            get { return _suit; }
        }
        public bool IsFaceUp
        {
            get { return _isFaceUp; }
        }

        #endregion

        public PlayingCard(int card)
        {
            _rank = card/13;
            _suit = card%13;
            _fullName = PrintRank() + " of " + PrintSuit();
            _isFaceUp = false;
        }

        public void FlipCard()
        {
            _isFaceUp = !_isFaceUp;
        }

        private string PrintRank()
        {
            if (_rank <= 8)
            {
                return _rank +"";
            }
            else
            {
                switch (_rank)
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

        private string PrintSuit()
        {
            switch (_suit)
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

        public int CompareTo(PlayingCard other)
        {
            if (this._rank < other._rank) return -1;
            if (this._rank > other._rank) return 1;
            return 0;
        }
    }
}
