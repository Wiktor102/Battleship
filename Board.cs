using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    enum BoardStatus { Empty, EmptyChecked, HitShip, Ship}
    internal abstract class Board
    {
        public BoardStatus[,] status = new BoardStatus[10, 10];

        public Board() {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    status[i, j] = BoardStatus.Empty;
                }
            }
        }

        virtual public void Display()
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

                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.Write(GetBoardStatusChar(status[i, j]));
                    Console.Write(' ');
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                Console.Write("\n");
            }
        }

        public static char GetBoardStatusChar(BoardStatus bs)
        {
            switch (bs)
            {
                case BoardStatus.Empty:
                    return '~';
                case BoardStatus.EmptyChecked:
                    return 'O';
                case BoardStatus.HitShip:
                    return 'X';
                case BoardStatus.Ship:
                    return '@';
                default:
                    return ' ';
            }
        }
    }
}
