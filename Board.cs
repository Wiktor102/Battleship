using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    internal class Board
    {
        public BoardCell[,] status = new BoardCell[10, 10];

        public Board()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    status[i, j] = new BoardCell();
                }
            }
        }

        virtual public void Display(bool showShipPositions = true)
        {
            for (int i = -1; i < 10; i++)
            {
                for (int j = -1; j < 10; j++)
                {
                    if (i == -1 && j == -1)
                    {
                        Console.Write("   ");
                        continue;
                    }

                    if (i == -1)
                    {
                        Console.Write(Convert.ToChar(j + (int)'A'));
                        Console.Write(' ');
                        continue;
                    }

                    if (j == -1)
                    {
                        Console.Write(i + 1);
                        Console.Write(i + 1 == 10 ? " " : "  ");
                        continue;
                    }

                    status[i, j].Display(showShipPositions);
                }

                Console.Write("\n");
            }
        }
    }

    internal class BoardCell
    {
        public virtual bool IsHit { get; set; } = false;

        public virtual void Display(bool showShipPositions)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(!IsHit ? "~ " : "X ");

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    internal class ShipBoardCell : BoardCell
    {
        public override bool IsHit
        {
            get => IsHit;
            set {
                IsHit = value;
                IsSunken = ParentShip.CheckIfSunken();
            }
        }

        public bool IsSunken = false;
        public Ship ParentShip;

        public ShipBoardCell(Ship ship) {
            ParentShip = ship;
        }

        public override void Display(bool showShipPositions)
        {
            if (!showShipPositions && !IsHit)
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("~ ");
            }
            else
            {
                if (IsSunken) {
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("X ");
                } else if (IsHit)
                {
                    Console.BackgroundColor = showShipPositions ? ConsoleColor.Gray : ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("X ");
                } else
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.Write("  ");
                }
            }


            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
