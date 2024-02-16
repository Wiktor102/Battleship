using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    internal class Cord
    {
        public int x;
        public int y;

        public Cord(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public string AsText() { return $"{Convert.ToChar(x + (int)'A')}{y + 1}"; }

        public static Cord PromptForCord()
        {
            var valid = false;
            int i = -1;

            do
            {
                i++;
                if (i > 0)
                {
                    IO.DisplayError("Niepoprawne pole, wybierz ponownie: ", true);
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                var cordAsString = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
                if (cordAsString.Length != 2 && cordAsString.Length != 3) continue;

                var letter = cordAsString.Substring(0, 1);
                if (letter.ToCharArray()[0] - 'A' < 0 || letter.ToCharArray()[0] - 'A' > 10) continue;

                var numberAsString = cordAsString.Substring(1, cordAsString.Length - 1);
                int num;
                if (!Int32.TryParse(numberAsString, out num) || num < 1 || num > 10) continue;

                return new Cord(letter.ToCharArray()[0] - 'A', num - 1);
            } while (!valid);

            throw new Exception("Something went wrong!");
        }
    }
}
