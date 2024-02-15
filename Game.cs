using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    internal class Game
    {
        private Player[] _players;
        private Player _currentPlayer;
        private int round = 0;

        public Game(Player[] players) {
            foreach (var player in players)
            {
                player.ResetBoards();
            }

            this._players = players;
            _currentPlayer = _players[0];

            Display();


            while (!CheckIfGameEnded())
            {
                if (round == 0)
                {
                    SetUpShips();
                }

                ChangePlayer();
            }
        }

        public void SetUpShips()
        {
            Console.WriteLine("Na początek musisz wybrać położenie swoich statków.");

        }

        public void Display()
        {
            Console.Clear();
            Console.WriteLine($"Teraz gra: {_currentPlayer.Name}");
            Console.WriteLine($"Wyniki:   {_players[0].Name}: {_players[0].WonGames} | {_players[1].Name}: {_players[1].WonGames}");
            Console.WriteLine("------------------------------------------------------------------------------------------------\n");

            _currentPlayer.MyBoard.Display();
            Console.Write('\n');
            _currentPlayer.NotMyBoard.Display();
        }

        public void ChangePlayer()
        {
            if (_currentPlayer.Name == _players[0].Name)
            {
                _currentPlayer = _players[1];
            } else
            {
                _currentPlayer = _players[0];
                round++;
            }
        }

        public bool CheckIfGameEnded()
        {
            return round > 0;
        }
    }
}
