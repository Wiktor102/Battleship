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
            for (int i = 0; true; i++) {
                if (i > 0)
                {
                    IO.DisplayError("Niepoprawne pole, wybierz ponownie: ", true);
                }

                var cordAsString = IO.ReadLineColored(ConsoleColor.Yellow);
				if (cordAsString.Length != 2 && cordAsString.Length != 3) continue;

				var letter = cordAsString.Substring(0, 1);
				if (letter.ToCharArray()[0] - 'A' < 0 || letter.ToCharArray()[0] - 'A' > 10) continue;

                var numberAsString = cordAsString.Substring(1, cordAsString.Length - 1);
                if (!Int32.TryParse(numberAsString, out int num) || num < 1 || num > 10) continue;

                return new Cord(letter.ToCharArray()[0] - 'A', num - 1);
            }

            throw new Exception("Something went wrong!");
        }
    }
}
