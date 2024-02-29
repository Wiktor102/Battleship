using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship {
    internal class MainMenu {
        Player[] Players = new Player[] { new Player("Gracz 1"), new Player("Gracz 2") };

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

            Console.WriteLine("1. Multiplayer");
            Console.WriteLine("2. Zagraj z komputerem");
            Console.WriteLine("3. Ustawienia");
            Console.WriteLine("4. Legenda");
            Console.WriteLine("5. Wyjdź");
        }

        private bool ReadKey() {
            bool correctKey = false;
            do {
                switch (Console.ReadKey().KeyChar) {
                    case '1':
                        correctKey = true;
                        Game.RunGame(Players);
                        break;
                    case '2':
                        correctKey = true;
                        break;
                    case '3':
                        correctKey = true;
                        break;
                    case '4':
                        correctKey = true;
                        break;
                    case '5':
                        return false;

                }
            } while (!correctKey);

            return true;
        }
    }
}
