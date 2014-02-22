using System;
using System.Drawing;

namespace OOP_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            House finalHouse = new House("Up the street");
            finalHouse.OpenDoor();

            Console.WriteLine(finalHouse.IsDoorOpen);

            finalHouse.AddRoom(10, 10);

            Console.WriteLine(finalHouse.SquareFootage);
        }
    }
}
