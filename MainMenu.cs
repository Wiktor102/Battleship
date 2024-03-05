using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship {
    internal class MainMenu {
        private int _selectedOption = 1;
		private string[] _options = new string[] { "Multiplayer", "Zagraj z komputerem", "Ustawienia", "Legenda", "Wyjdź" };

        public MainMenu() {
            do {
                Display();
            } while (ReadKey());
        }

        private void Display() {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(" $$$$$$\\    $$\\                $$\\     $$\\       $$\\");
            Console.WriteLine("$$  __$$\\   $$ |               $$ |    $$ |      \\__|");
            Console.WriteLine("$$ /  \\__|$$$$$$\\    $$$$$$\\ $$$$$$\\   $$ |  $$\\ $$\\");
            Console.WriteLine("\\$$$$$$\\  \\_$$  _|   \\____$$\\\\_$$  _|  $$ | $$  |$$ |");
            Console.WriteLine(" \\____$$\\   $$ |     $$$$$$$ | $$ |    $$$$$$  / $$ |");
            Console.WriteLine("$$\\   $$ |  $$ |$$\\ $$  __$$ | $$ |$$\\ $$  _$$<  $$ |");
            Console.WriteLine("\\$$$$$$  |  \\$$$$  |\\$$$$$$$ | \\$$$$  |$$ | \\$$\\ $$ |");
            Console.WriteLine(" \\______/    \\____/  \\_______|  \\____/ \\__|  \\__|\\__|\n");
            Console.ForegroundColor = ConsoleColor.White;


            for (int i = 1; i <= _options.Length; i++) {
                string msg = $" {i}. {_options[i - 1]} ";
				if (i == _selectedOption) {
                    IO.DisplayColored(msg,  ConsoleColor.Black, ConsoleColor.White);
                    continue;
				}

                Console.WriteLine(msg);
			}

            IO.DisplayColored("\n↑/↓ nawigacja   ↲ zatwierdż", ConsoleColor.DarkGray);
        }

        private bool ReadKey() {
            do {
                var pressedKey = Console.ReadKey(true);

				if (pressedKey.Key == ConsoleKey.UpArrow) {
					_selectedOption = _selectedOption == 1 ? _options.Length : _selectedOption - 1;
                    Display();
				}

                if (pressedKey.Key == ConsoleKey.DownArrow) {
					_selectedOption = _selectedOption == _options.Length ? 1 : _selectedOption + 1;
                    Display();
				}

                if (pressedKey.Key != ConsoleKey.Enter) continue;

                switch (_selectedOption) {
                    case 1:
                        Game.RunGame();
                        break;
                    case 2:
                        Game.RunGame(true);
                        break;
                    case 3:
                        break;
                    case 4:
                        DisplayLegendMenu();
						break;
                    case 5:
                        return false;

                }

                Display();
            } while (true);
        }

        private void DisplayLegendMenu() {
            Console.Clear();
            IO.DisplayTitle(new []{
				" _                               _         ",
				"| |                             | |      _ ",
				"| |     ___  __ _  ___ _ __   __| | __ _(_)",
				"| |    / _ \\/ _` |/ _ \\ '_ \\ / _` |/ _` |  ",
				"| |___|  __/ (_| |  __/ | | | (_| | (_| |_ ",
				"|______\\___|\\__, |\\___|_| |_|\\__,_|\\__,_(_)",
				"            |___/                          \n"
			});

            IO.DisplaySuccess("Twoja plansza:");
            IO.DisplayColored(" ~  ", ConsoleColor.White, ConsoleColor.Blue, true);
            Console.WriteLine(" - nieodkryte pole");

            IO.DisplayColored(" X  ", ConsoleColor.White, ConsoleColor.Blue, true);
            Console.WriteLine(" - puste pole (w krótre oddano strzał)");

            DisplayFieldExample(" ", ConsoleColor.White);
			Console.WriteLine(" - statek (nietrafiony)");

            DisplayFieldExample("X", ConsoleColor.Gray, ConsoleColor.DarkRed);
            Console.WriteLine(" - zastrzelona część statku");

            DisplayFieldExample("X", ConsoleColor.DarkGray, ConsoleColor.Red);
            Console.WriteLine(" - zatopiony statek");


			IO.DisplaySuccess("\nPlansza przeciwnika:");
            IO.DisplayColored(" ~  ", ConsoleColor.White, ConsoleColor.Blue, true);
            Console.WriteLine(" - nieodkryte pole");

            IO.DisplayColored(" O  ", ConsoleColor.White, ConsoleColor.Blue, true);
            Console.WriteLine(" - puste pole (odkryte)");

			DisplayFieldExample("X", ConsoleColor.Gray, ConsoleColor.DarkGreen);
			Console.WriteLine(" - zastrzelona część statku");

			DisplayFieldExample("X", ConsoleColor.DarkGray, ConsoleColor.Green);
			Console.WriteLine(" - zatopiony statek");

            IO.DisplayColored("\nNaciśnij dowlony klawisz by wrócić...", ConsoleColor.DarkGray);
            Console.ReadKey(true);

            void DisplayFieldExample (string symbol, ConsoleColor bg, ConsoleColor fg = ConsoleColor.White) {
				IO.DisplayColored(" ", ConsoleColor.White, ConsoleColor.Blue, true);
				IO.DisplayColored(symbol + " ", fg, bg, true);
				IO.DisplayColored(" ", ConsoleColor.White, ConsoleColor.Blue, true);
			}
		}
    }
}
