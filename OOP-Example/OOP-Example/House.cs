using System.Drawing;
using System.Collections.Generic;

namespace OOP_Example
{
    public class House
    {
        #region Fields

        private string _address;
        private Color _color;
        private bool _isDoorOpen;

        #endregion

        #region Properties

        private List<Room> _rooms;

        public string Address
        {
            get { return _address; }
            set 
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    _address = "Address missing";
                }
                else
                {
                    _address = value;
                }
            }
        }

        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }

        public int SquareFootage
        {
            get
            {
                int squareFootage = 0;

                foreach (Room space in _rooms)
                {
                    squareFootage += space.GetSquareFootage();
                }

                return squareFootage;
            }
        }

        public bool IsDoorOpen
        {
            get { return _isDoorOpen; }
        }

        #endregion

        #region initialization

        public House(string address)
        {
            _rooms = new List<Room>();

            _address = address;
        }

        #endregion

        public override string ToString()
        {
            return ("This house is at " + _address + " and it's color is " + _color.ToKnownColor());
        }

        public void AddRoom(int length, int width)
        {
            _rooms.Add(new Room(length, width));
        }

        public void OpenDoor()
        {
            _isDoorOpen = true;
        }

        public void CloseDoor()
        {
            _isDoorOpen = false;
        }

        public void ToggleDoor()
        {
            _isDoorOpen = !_isDoorOpen;
        }
    }
}
