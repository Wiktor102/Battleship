using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship {
    internal class MainMenu {
        Player[] Players = new Player[] { new Player("Gracz 1"), new Player("Gracz 2") };

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

            Console.WriteLine("↑/↓ nawigacja   ↲ zatwierdż");
        }

        private bool ReadKey() {
            do {
                var pressedKey = Console.ReadKey();

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
                        Game.RunGame(Players);
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        return false;

                }
            } while (true);
        }
    }
}
