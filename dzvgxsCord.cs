using System;


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

		public AsText () { return $"{Convert.ToChar(y + (int)'A')}{x}"; }

        static Cord PromptForCord(string errorMsg)
		{
			var valid = false;
			int i = -1;
			do
			{
				i++;
				if (i > 0)
				{
					Console.Write(errorMsg);
				}

				var cordAsString = Console.ReadLine();
				if (cordAsString.Length != 2 && cordAsString.Length != 3) continue;

				var letter = cordAsString.Substring(0, 1);
				if (letter - 'A' < 0 || letter - 'A' > 10) continue;

				var numberAsString = letter.Substring(1, cordAsString.Length - 1);
				int num;
				if (!Int32.TryParse(numberAsString, out num) || num < 0 || num > 10) continue;

				return new Cord(num, letter - 'A');
			} while (!valid);
		}
	}
}
