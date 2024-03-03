using System;
using System.Collections.Generic;
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

		public static Ship Place(Board board, int shipLength, int shipNumber) {
			Cord firstFieldCord;
			Direction dir;
			int i = 0;
			bool correct = true;

			do {
				if (i > 0) {
					IO.DisplayError("\nWybrałeś niepoprawną pozycję statku!");
				}

				Console.Write($"Wybierz pole by umieścić statek {shipLength} masztowy nr {shipNumber}: ");
				firstFieldCord = Cord.PromptForCord();
				dir = shipLength == 1 ? Direction.Horizontal : IO.PromptForDirection();

				for (int j = firstFieldCord.y - 1; j < (dir == Direction.Horizontal ? 3 : firstFieldCord.y + shipLength + 1) && j < 10; j++) {
					if (j < 0) continue;
					for (int k = firstFieldCord.x - 1; k < (dir == Direction.Vertical ? 3 : firstFieldCord.x + shipLength + 1) && k < 10; k++) {
						if (k < 0) continue;
						if (k > 10 || board.status[j, k] is ShipBoardCell) {
							correct = false;
							break;
						}
					}
				}

				i++;
			} while (!correct);

			return new Ship(shipLength, firstFieldCord, dir, board);
		}

	}
}
