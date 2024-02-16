using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    internal abstract class IO
    {
        public static void DisplaySuccess(string msg, bool inline = false)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            if (inline) Console.Write(msg);  else Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void DisplayWarning(string msg, bool inline = false)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            if (inline) Console.Write(msg);  else Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void DisplayError(string msg, bool inline = false)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            if (inline) Console.Write(msg);  else Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.White;
        }


        public static Direction PromptForDirection()
        {
            int i = 0;
            do
            {
                if (i > 0)
                {
                    Console.Write("\nPodano błędną wartość. Podaj kierunek statku (H / V): ");
                } else
                {
                    Console.Write("Podaj kierunek statku (H / V): ");
                }
                
                switch (Console.ReadKey().KeyChar)
                {
                    case 'H':
                    case 'h':
                        return Direction.Horizontal;
                    case 'V':
                    case 'v':
                        return Direction.Vertical;
                }

                i++;
            } while (true);
        }
    }
}
