using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Battleship {
	enum Direction { Horizontal, Vertical };
	internal class Game {
		//private enum DisplayMode { Normal, Wait };
		//private DisplayMode _displayMode = DisplayMode.Normal;

		private readonly Player[] _players;
		private Player _currentPlayer;
		private Player _otherPlayer;

		public static Dictionary<int, int> AllShips = new Dictionary<int, int> {
			//[1] = 4,
			//[2] = 3,
			//[3] = 2,
			//[4] = 1,
			[1] = 1,
			[3] = 1,
		};

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
				Display(round != 0);

				if (round == 0) {
					_currentPlayer.SetUpShips(Display);
				} else {
					gameEnded = Turn();
				}

				if (gameEnded) break;
				ChangePlayer();
			}
		}

		private bool Turn() {
			ShootResult result;
			bool gameEnded;

			do {
				if (!(_currentPlayer is Computer)) Console.Write("Wybierz pole do strzału (np. A1): ");
				result = _currentPlayer.Shoot(_otherPlayer.board);
				Display();

				gameEnded = CheckIfGameEnded();

				switch (result) {
					case ShootResult.FullSuccess:
						if (!(_currentPlayer is Computer)) IO.DisplaySuccess("Udało ci się zatopić statek przeciwnika!");
						if (gameEnded) goto endTurn;
						continue;
					case ShootResult.Success:
						if (!(_currentPlayer is Computer)) IO.DisplaySuccess("Udało ci się trafić w statek przeciwnika!");
						if (gameEnded) goto endTurn;
						continue;
					case ShootResult.Failure:
						if (!(_currentPlayer is Computer)) IO.DisplayWarning("Nie udało ci się trafić w statek przeciwnika!");
						break;

				}
			} while (result != ShootResult.Failure);

			endTurn:
			return gameEnded;
		}

		public void ChangePlayer() {
			Console.ForegroundColor = ConsoleColor.Yellow;
			if (_otherPlayer is Computer) {
				Player tmp = _currentPlayer;
				_currentPlayer = _otherPlayer;
				_otherPlayer = tmp;

				Console.Clear();
				Console.WriteLine($"Teraz gra {_currentPlayer.Name}...");
				return;
			} else if (_currentPlayer is Computer) {
				Thread.Sleep((new Random()).Next(700, 1100));

				Console.Clear();
				Console.WriteLine($"{_currentPlayer.Name} zakończył swoją turę");
				Console.WriteLine("Naciśnij dowolny klawisz by rozpocząć swoją turę...");
				Console.ReadKey(true);

				Player tmp = _currentPlayer;
				_currentPlayer = _otherPlayer;
				_otherPlayer = tmp;
				round++;

				Console.ResetColor();
				return;
			}

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
			Console.ResetColor();
		}

		private bool CheckIfGameEnded() {
			if (_players[0].GetTotalRamaningShips() == 0 || _players[1].GetTotalRamaningShips() == 0) {
				return true;
			}

			return false;
		}

		private void Display(bool showEnemyBoard = true) {
			if (_currentPlayer is Computer) return;
			Console.Clear();

			DisplayCurrentPlayer();
			if (round > 0) DisplayRemainingShips();

			Console.WriteLine("Twoja plansza:");
			_currentPlayer.board.Display();

			if (showEnemyBoard) {
				Console.WriteLine("\nPlansza przeciwnika:");
				_otherPlayer.board.Display(false);
			}
		}

		private void DisplayCurrentPlayer() {
			Console.Write("Teraz gra: ");
			IO.DisplaySuccess(_currentPlayer.Name, true);
			Console.WriteLine($"          Wyniki:  {_players[0].Name}: {_players[0].WonGames} | {_players[1].Name}: {_players[1].WonGames}");
			Console.WriteLine("--------------------------------------------------------------");
		}

		private void DisplayRemainingShips() {
			var remainingShips = _otherPlayer.GetRemaingShipsCountByCategory();

			Console.WriteLine("Pozostałe statki przeciwnika:");
			Console.WriteLine($"_◢__  x{remainingShips[1]}      _◢▇___  x{remainingShips[2]}      _◢▇▇____  x{remainingShips[3]}      __◢▇▇▇_____  x{remainingShips[4]}");
			Console.WriteLine("\\__/          \\____/          \\______/          \\_________/");
			Console.WriteLine("--------------------------------------------------------------\n");
		}

		public static void RunGame(bool withComputer = false) {
			Player[] players = new Player[] { new Player("Gracz 1"), withComputer ? new Computer() : new Player("Gracz 2") };

			while (true) {
				new Game(players);
				IO.DisplayWarning("Gra zakończona. Czy chcesz zagrać ponownie? (T/N)");
				if (!IO.PromptForBool()) break;
			}
		}
	}
}
