using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship {
	internal class Board {
		public BoardCell[,] status = new BoardCell[10, 10];

		public Board() {
			for (int i = 0; i < 10; i++) {
				for (int j = 0; j < 10; j++) {
					status[i, j] = new BoardCell();
				}
			}
		}

		virtual public void Display(bool isOwnBoard = true) {
			for (int i = -1; i < 10; i++) {
				for (int j = -1; j < 10; j++) {
					if (i == -1 && j == -1) {
						Console.Write("   ");
						continue;
					}

					if (i == -1) {
						Console.Write(Convert.ToChar(j + (int)'A'));
						Console.Write(' ');
						continue;
					}

					if (j == -1) {
						Console.Write(i + 1);
						Console.Write(i + 1 == 10 ? " " : "  ");
						continue;
					}

					status[i, j].Display(isOwnBoard);
				}

				Console.Write("\n");
			}
		}

		public void SurroundShip(Ship ship) {
			int firstX = ship.FirstCord.x;
			int firstY = ship.FirstCord.y;

			if (ship.ShipDirection == Direction.Horizontal) {
				if (firstX - 1 >= 0) status[firstY, firstX - 1]?.Hit();
				if (firstX + ship.Size + 1 < 10) status[firstY, firstX + ship.Size]?.Hit();

				for (int i = firstX - 1; i <= firstX + ship.Size; i++) {
					if (i > 10 || i < 0) continue;
					if (firstY - 1 >= 0) status[firstY - 1, i]?.Hit();
					if (firstY + 1 < 10) status[firstY + 1, i]?.Hit();
				}
			} else {
				if (firstY - 1 >= 0) status[firstY - 1, firstX]?.Hit();
				if (firstY + ship.Size + 1 < 10) status[firstY + ship.Size, firstX]?.Hit();

				for (int i = firstY - 1; i <= firstY + ship.Size; i++) {
					if (i > 10 || i < 0) continue;
					if (firstX - 1 >= 0) status[i, firstX - 1]?.Hit();
					if (firstX + 1 < 10) status[i, firstX + 1]?.Hit();
				}
			}
		}
	}

	internal class BoardCell {
		public virtual bool IsHit { get; set; } = false;

		public void Hit() {
			IsHit = true;
		}

		public virtual void Display(bool isOwnBoard) {
			if (!IsHit) {
				IO.DisplayColored("~ ", ConsoleColor.White, ConsoleColor.Blue, true);
				return;
			}

			if (isOwnBoard) {
				IO.DisplayColored("X ", ConsoleColor.White, ConsoleColor.Blue, true);
			} else {
				IO.DisplayColored("O ", ConsoleColor.White, ConsoleColor.Blue, true);
			}
		}
	}

	internal class ShipBoardCell : BoardCell {
		private bool _isHit = false;
		public override bool IsHit {
			get => _isHit;
			set {
				_isHit = value;
				ParentShip.CheckIfSunken();
			}
		}

		public Ship ParentShip;

		public ShipBoardCell(Ship ship) {
			ParentShip = ship;
		}

		public override void Display(bool isOwnBoard) {
			if (!IsHit && isOwnBoard) {
				IO.DisplayColored("  ", ConsoleColor.White, ConsoleColor.White, true);
				return;
			}

			if (!IsHit && !isOwnBoard) {
				IO.DisplayColored("~ ", ConsoleColor.White, ConsoleColor.Blue, true);
				return;
			}

			if (ParentShip.IsSunken) {
				IO.DisplayColored("X ", isOwnBoard ? ConsoleColor.DarkRed : ConsoleColor.Green, ConsoleColor.DarkGray, true);
				return;
			}

			if (IsHit) {
				IO.DisplayColored("X ", isOwnBoard ? ConsoleColor.DarkRed : ConsoleColor.DarkGreen, ConsoleColor.Gray, true);
				return;
			}
		}
	}
}
