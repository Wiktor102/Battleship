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

        public void PlaceShip(int shipLength, int shipNumber)
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
                firstFieldCord = Cord.PromptForCord("Niepoprawne pole, wybierz ponownie: ");
                dir = shipLength == 1 ? Direction.Horizontal : IO.PromptForDirection();

                for (int j = firstFieldCord.y - 1; j < (dir == Direction.Horizontal ? 3 : firstFieldCord.y + shipLength + 1) && j < 10; j++)
                {
                    if (j < 0) continue;
                    for (int k = firstFieldCord.x - 1; k < (dir == Direction.Vertical ? 3 : firstFieldCord.x + shipLength + 1) && k < 10; k++)
                    {
                        if (k < 0) continue;
                        if (k > 10 || status[j, k] != BoardStatus.Empty)
                        {
                            correct = false;
                            break;
                        }
                    }
                }

                i++;
            } while (!correct);

            for (int j = firstFieldCord.x; j < shipLength; j++)
            {
                if (dir == Direction.Horizontal)
                {
                    status[firstFieldCord.y, j] = BoardStatus.Ship;
                } else
                {
                    status[j, firstFieldCord.x] = BoardStatus.Ship;
                }
            }
        }
    }
}
