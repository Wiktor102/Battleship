using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    internal abstract class IO
    {
        public static void DisplayColored(string msg, ConsoleColor fg, ConsoleColor bg = ConsoleColor.Black, bool inline = false)
        {
            Console.ForegroundColor = fg;
            Console.BackgroundColor = bg;
            if (inline) Console.Write(msg); else Console.WriteLine(msg);
            Console.ResetColor();
        }

        public static string ReadLineColored(ConsoleColor fg, ConsoleColor bg = ConsoleColor.Black) {
            Console.ForegroundColor = fg;
            Console.BackgroundColor = bg;
            string input = Console.ReadLine();
            Console.ResetColor();
            return input;
        }

        public static void DisplaySuccess(string msg, bool inline = false)
        {
            IO.DisplayColored(msg, ConsoleColor.Green, ConsoleColor.Black, inline);
        }
        public static void DisplayWarning(string msg, bool inline = false)
        {
            IO.DisplayColored(msg, ConsoleColor.Yellow, ConsoleColor.Black, inline);
        }

        public static void DisplayError(string msg, bool inline = false)
        {
            IO.DisplayColored(msg, ConsoleColor.Red, ConsoleColor.Black, inline);
        }

        public static bool PromptForBool() {
            int i = 0;
            while (true) {
                if (i > 0) {
                    IO.DisplayError("Wprowadzono złą wartość. Wyperz ponownie: (T / N)", true);
                }

                i++;

                switch (Console.ReadKey().KeyChar) {
                    case 'T':
                    case 't':
                        return true;
                    case 'N':
                    case 'n':
                        return false;
                }
            };

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
