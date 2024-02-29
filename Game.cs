﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Battleship {
    enum Direction { Horizontal, Vertical };
    internal class Game {
        //private enum DisplayMode { Normal, Wait };
        //private DisplayMode _displayMode = DisplayMode.Normal;

        private readonly Player[] _players;
        private Player _currentPlayer;
        private Player _otherPlayer;

        private int round = 0;

        public Game(Player[] players) {
            this._players = players;
            _currentPlayer = _players[0];
            _otherPlayer = _players[1];

            ResetBoards();
            GameLoop();

            Player winner = _currentPlayer;
            winner.WonGames++;
            IO.DisplaySuccess($"Wygrał {winner.Name} \n");
        }

        private void ResetBoards() {
            foreach (var player in _players) {
                player.ResetBoards();
            }
        }

        private void GameLoop() {
            while (true) {
                bool gameEnded = false;
                Display();

                if (round == 0) {
                    SetUpShips();
                } else {
                    gameEnded = Turn();
                }

                if (gameEnded) break;
                ChangePlayer();
            }
        }

        private void SetUpShips() {

            foreach (KeyValuePair<int, bool[]> entry in _currentPlayer.ShipsSetUp) {
                var shipLength = entry.Key;

                for (int i = 0; i < entry.Value.Length; i++) {
                    _currentPlayer.Ships.Add(Ship.Place(_currentPlayer.board, shipLength, i + 1));
                    Console.Clear();
                    Display();
                }
            }

        }

        private bool Turn() {
            ShootResult result;
            bool gameEnded;

            do {
                Console.Write("Wybierz pole do strzału: ");
                result = _currentPlayer.Shoot(_otherPlayer.board);
                Display();

                gameEnded = CheckIfGameEnded();

                switch (result) {
                    case ShootResult.FullSuccess:
                        IO.DisplaySuccess("Udało ci się zatopić statek przeciwnika!");
                        if (gameEnded) break;
                        continue;
                    case ShootResult.Success:
                        IO.DisplaySuccess("Udało ci się trafić w statek przeciwnika!");
                        if (gameEnded) break;
                        continue;
                    case ShootResult.Failure:
                        IO.DisplayWarning("Nie udało ci się trafić w statek przeciwnika!");
                        break;

                }
            } while (result != ShootResult.Failure);

            return gameEnded;
        }

        public void ChangePlayer() {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\nTwoja tura dobiegła końca. Naciśnij ENTER by przejść dalej...");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine($"{_currentPlayer.Name} zakończył swoją turę.");


            if (_currentPlayer.Name == _players[0].Name) {
                _currentPlayer = _players[1];
                _otherPlayer = _players[0];
                Console.Write("Graczu 2, naciśnij ENTER by rozpocząć swoją turę...");
            } else {
                _currentPlayer = _players[0];
                _otherPlayer = _players[1];
                Console.Write("Graczu 1, naciśnij ENTER by rozpocząć swoją turę...");
                round++;
            }

            Console.ReadLine();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
        }

        private bool CheckIfGameEnded() {
            if (_players[0].GetTotalRamaningShips() == 0 || _players[1].GetTotalRamaningShips() == 0) {
                return true;
            }

            return false;
        }

        private void Display() {
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
    
        public static void RunGame(Player[] players) {
            while (true) {
                new Game(players);
                IO.DisplayWarning("Gra zakończona. Czy chcesz zagrać ponownie? (T/N)");
                if (!IO.PromptForBool()) break;
            }
        }
    }
}
