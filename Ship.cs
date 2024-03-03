using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship {
	internal class Ship {

		public int Size;
		public bool IsSunken = false;

		public Cord FirstCord;
		public Direction ShipDirection;

		private readonly List<ShipBoardCell> _shipCells = new List<ShipBoardCell>();

		public Ship(int size, Cord firstCell, Direction dir, Board board) {
			Size = size;
			FirstCord = firstCell;
			ShipDirection = dir;

			for (int i = 0; i < Size; i++) {
				ShipBoardCell cell = new ShipBoardCell(this);
				_shipCells.Add(cell);

				if (dir == Direction.Horizontal) {
					board.status[firstCell.y, firstCell.x + i] = cell;
				} else {
					board.status[firstCell.y + i, firstCell.x] = cell;
				}
			}
		}
		public bool CheckIfSunken() {
			Console.WriteLine(_shipCells.Count);
			foreach (var cell in _shipCells) {
				if (!cell.IsHit) return false;
			}

			IsSunken = true;
			return true;
		}

		public static Ship Place(Board board, int shipSize, int shipNumber) {
			Cord firstFieldCord;
			Direction dir;

			for (int i = 0; true; i++) {
				if (i > 0) {
					IO.DisplayError("\nWybrałeś niepoprawną pozycję statku!");
				}

				Console.Write("Wybierz pole by umieścić statek ");
				IO.DisplayColored(shipNumber.ToString(), ConsoleColor.Cyan, ConsoleColor.Black, true);
				Console.Write(" masztowy nr ");
				IO.DisplayColored(shipSize.ToString(), ConsoleColor.Cyan, ConsoleColor.Black, true);
				Console.Write(" (np. A1): ");

				firstFieldCord = Cord.PromptForCord();
				dir = shipSize == 1 ? Direction.Horizontal : IO.PromptForDirection();

				if (dir == Direction.Horizontal) {
					if (firstFieldCord.x - 1 >= 0 && board.status[firstFieldCord.y, firstFieldCord.x - 1] is ShipBoardCell) goto next; // Cell to the left of the ship
					if (firstFieldCord.x + shipSize + 1 < 10 && board.status[firstFieldCord.y, firstFieldCord.x + shipSize + 1] is ShipBoardCell) goto next; // Cell to the right of the ship

					for (int j = firstFieldCord.x - 1; j <= firstFieldCord.x + shipSize; j++) {
						if (j > 10 || j < 0) continue;
						if (firstFieldCord.y - 1 >= 0 && board.status[firstFieldCord.y - 1, j] is ShipBoardCell) goto next; // Row above the ship
						if (firstFieldCord.y + 1 < 10 && board.status[firstFieldCord.y + 1, j] is ShipBoardCell) goto next; // Row below the ship
					}
				} else {
					if (firstFieldCord.y - 1 >= 0 && board.status[firstFieldCord.y - 1, firstFieldCord.x] is ShipBoardCell) goto next; // Cell above the ship
					if (firstFieldCord.y + shipSize + 1 < 10 && board.status[firstFieldCord.y + shipSize + 1, firstFieldCord.x] is ShipBoardCell) goto next; // Cell below the ship

					for (int j = firstFieldCord.y - 1; j <= firstFieldCord.y + shipSize; j++) {
						if (j > 10 || j < 0) continue;
						if (firstFieldCord.x - 1 >= 0 && board.status[j, firstFieldCord.x - 1] is ShipBoardCell) goto next; // Left collumn
						if (firstFieldCord.x + 1 < 10 && board.status[j, firstFieldCord.x + 1] is ShipBoardCell) goto next; // Right collumn
					}
				}

				break;
				next: continue;
			}

			return new Ship(shipSize, firstFieldCord, dir, board);
		}

	}
}
