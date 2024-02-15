using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    internal class OwnBoard: Board
    {
        override public void Display() {
            Console.WriteLine("Twoja plansza:");
            base.Display();
        }
    }
}
