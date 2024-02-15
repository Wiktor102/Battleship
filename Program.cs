using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Game CurrentGame;
            Player[] players = { new Player("Gracz 1"), new Player("Gracz 2") };

            {
                Console.WriteLine(" $$$$$$\\    $$\\                $$\\     $$\\       $$\\");
                Console.WriteLine("$$  __$$\\   $$ |               $$ |    $$ |      \\__|");
                Console.WriteLine("$$ /  \\__|$$$$$$\\    $$$$$$\\ $$$$$$\\   $$ |  $$\\ $$\\");
                Console.WriteLine("\\$$$$$$\\  \\_$$  _|   \\____$$\\\\_$$  _|  $$ | $$  |$$ |");
                Console.WriteLine(" \\____$$\\   $$ |     $$$$$$$ | $$ |    $$$$$$  / $$ |");
                Console.WriteLine("$$\\   $$ |  $$ |$$\\ $$  __$$ | $$ |$$\\ $$  _$$<  $$ |");
                Console.WriteLine("\\$$$$$$  |  \\$$$$  |\\$$$$$$$ | \\$$$$  |$$ | \\$$\\ $$ |");
                Console.WriteLine(" \\______/    \\____/  \\_______|  \\____/ \\__|  \\__|\\__|\n");

                Console.WriteLine("1. Multiplayer");
                Console.WriteLine("2. Zagraj z komputerem");
                Console.WriteLine("3. Ustawienia");
                Console.WriteLine("4. Legenda");
                Console.WriteLine("5. Wyjdź");

                bool correctKey = false;
                int i = 0;
                do
                {
                    switch (Console.ReadKey().KeyChar)
                    {
                        case '1':
                            correctKey = true;
                            CurrentGame = new Game(players);
                            Console.WriteLine("\n Press any key to exit...");
                            Console.ReadKey();
                            break;
                        case '2':
                            correctKey = true;
                            Console.WriteLine("\n Press any key to exit...");
                            Console.ReadKey();
                            break;
                        case '3':
                            correctKey = true;
                            break;
                        case '4':
                            correctKey = true;
                            break;
                        case '5':
                            correctKey = true;
                            break;

                    }
                    i++;
                } while (!correctKey);
            }
        }
    }
}
