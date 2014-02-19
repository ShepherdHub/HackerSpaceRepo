using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOP_Example
{
    public class Room
    {
        private int _length;
        private int _width;

        public int Length
        {
            get { return _length; }
            set { _length = value; }
        }

        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        /// <summary>
        /// Builds a room
        /// </summary>
        /// <param name="length">How long the room is in whole feet.</param>
        /// <param name="width">DOY! The width in whole feet.</param>
        public Room(int length, int width)
        {
            _length = length;
            _width = width;
        }

        /// <summary>
        /// This method is COOOL!
        /// </summary>
        /// <returns>What ever I want!</returns>
        public int GetSquareFootage()
        {
            return _length * _width;
        }
    }
}
