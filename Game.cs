using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    enum Direction { Horizontal, Vertical};
    internal class Game
    {
        //private enum DisplayMode { Normal, Wait };
        //private DisplayMode _displayMode = DisplayMode.Normal;

        private Player[] _players;
        private Player _currentPlayer;
        private Player _otherPlayer;

        private int round = 0;

        public Game(Player[] players) {
            foreach (var player in players)
            {
                player.ResetBoards();
            }

            this._players = players;
            _currentPlayer = _players[0];
            _otherPlayer = _players[1];



            while (!CheckIfGameEnded())
            {
                Display();
                if (round == 0)
                {
                    SetUpShips();
                } else
                {
                    var result = _currentPlayer.Shoot(_otherPlayer.board);
                    Display();
                    switch (result)
                    {
                        case ShootSuccess.FullSuccess:
                            IO.DisplaySuccess("Udało ci się zatopić statek przeciwnika!");
                            continue;
                        case ShootSuccess.Success:
                            IO.DisplaySuccess("Udało ci się trafić w statek przeciwnika!");
                            continue;
                        case ShootSuccess.Failure:
                            IO.DisplayWarning("Nie udało ci się trafić w statek przeciwnika!");
                            break;
                        
                    }
                }

                ChangePlayer();
            }
        }

        public void SetUpShips()
        {

            foreach (KeyValuePair<int, bool[]> entry in _currentPlayer.ShipsSetUp)
            {
                var shipLength = entry.Key;
                for (int i = 0; i < entry.Value.Length; i++)
                {
                    _currentPlayer.Ships.Add(Ship.Place(_currentPlayer.board, shipLength, i + 1));
                    Console.Clear();
                    Display();
                }
            }

        }

        public void Display()
        {
            Console.Clear();
            Console.Write("Teraz gra: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(_currentPlayer.Name);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"          Wyniki:  {_players[0].Name}: {_players[0].WonGames} | {_players[1].Name}: {_players[1].WonGames}");
            Console.WriteLine("--------------------------------------------------------------\n");

            Console.WriteLine("Twoja plansza:");
            _currentPlayer.board.Display();
            Console.WriteLine("\nPlansza przeciwnika:");
            _otherPlayer.board.Display(false);
        }

        public void ChangePlayer()
        {
            Console.ForegroundColor= ConsoleColor.Yellow;
            Console.Write("\nTwoja tura dobiegła końca. Naciśnij ENTER by przejść dalej...");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine($"{_currentPlayer.Name} zakończył swoją turę.");


            if (_currentPlayer.Name == _players[0].Name)
            {
                _currentPlayer = _players[1];
                _otherPlayer = _players[0];
                Console.Write("Graczu 2, naciśnij ENTER by rozpocząć swoją turę...");
            } else
            {
                _currentPlayer = _players[0];
                _otherPlayer = _players[1];
                Console.Write("Graczu 1, naciśnij ENTER by rozpocząć swoją turę...");
                round++;
            }

            Console.ReadLine();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
        }

        public bool CheckIfGameEnded()
        {
            return round > 3;
        }
    }
}
