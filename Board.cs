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
					status[i, j] = new EmptyBoardCell();
				}
			}
		}

		public bool ValidateShipPlacement(Cord firstFieldCord, int shipSize, Direction dir) {
			if (dir == Direction.Horizontal) {
				if (firstFieldCord.x - 1 >= 0 && status[firstFieldCord.y, firstFieldCord.x - 1] is ShipBoardCell) return false; // Cell to the left of the ship
				if (firstFieldCord.x + shipSize + 1 < 10 && status[firstFieldCord.y, firstFieldCord.x + shipSize + 1] is ShipBoardCell) return false; // Cell to the right of the ship

				for (int j = firstFieldCord.x - 1; j <= firstFieldCord.x + shipSize; j++) {
					if (j > 10) return false; // Ship exceeds the board to the right
					if (j < 0 || j >= 10) continue;
					if (firstFieldCord.y - 1 >= 0 && status[firstFieldCord.y - 1, j] is ShipBoardCell) return false; // Row above the ship
					if (status[firstFieldCord.y, j] is ShipBoardCell) return false; // Row where the ship will be placed
					if (firstFieldCord.y + 1 < 10 && status[firstFieldCord.y + 1, j] is ShipBoardCell) return false; // Row below the ship
				}
			} else {
				if (firstFieldCord.y - 1 >= 0 && status[firstFieldCord.y - 1, firstFieldCord.x] is ShipBoardCell) return false; // Cell above the ship
				if (firstFieldCord.y + shipSize + 1 < 10 && status[firstFieldCord.y + shipSize + 1, firstFieldCord.x] is ShipBoardCell) return false; // Cell below the ship

				for (int j = firstFieldCord.y - 1; j <= firstFieldCord.y + shipSize; j++) {
					if (j > 10) return false; // Ship exceeds the board to the bottom
					if (j < 0 || j >= 10) continue;
					if (firstFieldCord.x - 1 >= 0 && status[j, firstFieldCord.x - 1] is ShipBoardCell) return false; // Left collumn
					if (status[j, firstFieldCord.x] is ShipBoardCell) return false; // Collumn where the ship will be placed
					if (firstFieldCord.x + 1 < 10 && status[j, firstFieldCord.x + 1] is ShipBoardCell) return false; // Right collumn
				}
			}

			return true;
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
				if (firstX - 1 >= 0) ((EmptyBoardCell)status[firstY, firstX - 1])?.MakrAsBlocked();
				if (firstX + ship.Size + 1 < 10) ((EmptyBoardCell)status[firstY, firstX + ship.Size])?.MakrAsBlocked();

				for (int i = firstX - 1; i <= firstX + ship.Size; i++) {
					if (i > 10 || i < 0) continue;
					if (firstY - 1 >= 0) ((EmptyBoardCell)status[firstY - 1, i])?.MakrAsBlocked();
					if (firstY + 1 < 10) ((EmptyBoardCell)status[firstY + 1, i])?.MakrAsBlocked();
				}
			} else {
				if (firstY - 1 >= 0) ((EmptyBoardCell)status[firstY - 1, firstX])?.MakrAsBlocked();
				if (firstY + ship.Size + 1 < 10) ((EmptyBoardCell)status[firstY + ship.Size, firstX])?.MakrAsBlocked();

				for (int i = firstY - 1; i <= firstY + ship.Size; i++) {
					if (i > 10 || i < 0) continue;
					if (firstX - 1 >= 0) ((EmptyBoardCell)status[i, firstX - 1])?.MakrAsBlocked();
					if (firstX + 1 < 10) ((EmptyBoardCell)status[i, firstX + 1])?.MakrAsBlocked();
				}
			}
		}
	}

	internal abstract class BoardCell {
		public virtual bool IsHit { get; set; } = false;


		public void Hit() {
			IsHit = true;
		}

		public abstract void Display(bool isOwnBoard);
	}

	internal class EmptyBoardCell : BoardCell {
		public bool IsBlocked { get; private set; } = false;

		public void MakrAsBlocked() {
			IsBlocked = true;
		}

		public override void Display(bool isOwnBoard) {
			if (IsBlocked) {
				IO.DisplayColored("# ", ConsoleColor.White, ConsoleColor.Blue, true);
				return;
			}

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
