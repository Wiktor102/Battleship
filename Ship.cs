using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    internal class Ship
    {
        
        public int Size;
        public bool Sunken = false;
        private List<ShipBoardCell> _shipCells = new List<ShipBoardCell>();

        public Ship(int size, Cord firstCell, Direction dir, Board board)
        {
            Size = size;

            for (int i = 0; i < Size; i++)
            {
                if (dir == Direction.Horizontal)
                {
                    board.status[firstCell.y, firstCell.x + i] = new ShipBoardCell(this);
                    continue;
                }

                _shipCells.Add(new ShipBoardCell(this));
                board.status[firstCell.y + i, firstCell.x] = _shipCells.Last();
            }
        }
        public bool CheckIfSunken() {
            foreach (var cell in _shipCells)
            {
                if (!cell.IsHit) return false;
            }

            Sunken = true;
            return true;
        }

        public static Ship Place(Board board, int shipLength, int shipNumber)
        {
            Console.WriteLine("Na początek musisz wybrać położenie swoich statków.");
            Cord firstFieldCord;
            Direction dir;
            int i = 0;
            bool correct = true;

            do
            {
                if (i > 0)
                {
                    IO.DisplayError("\nWybrałeś niepoprawną pozycję statku!");
                }

                Console.Write($"Wybierz pole by umieścić statek {shipLength} masztowy nr {shipNumber}: ");
                firstFieldCord = Cord.PromptForCord();
                dir = shipLength == 1 ? Direction.Horizontal : IO.PromptForDirection();

                for (int j = firstFieldCord.y - 1; j < (dir == Direction.Horizontal ? 3 : firstFieldCord.y + shipLength + 1) && j < 10; j++)
                {
                    if (j < 0) continue;
                    for (int k = firstFieldCord.x - 1; k < (dir == Direction.Vertical ? 3 : firstFieldCord.x + shipLength + 1) && k < 10; k++)
                    {
                        if (k < 0) continue;
                        if (k > 10 || board.status[j, k] is ShipBoardCell)
                        {
                            correct = false;
                            break;
                        }
                    }
                }

                i++;
            } while (!correct);

            return new Ship(shipLength, firstFieldCord, dir, board);
        }
    
    }
}
