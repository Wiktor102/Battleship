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

				if (board.ValidateShipPlacement(firstFieldCord, shipSize, dir)) break;
			}

			return new Ship(shipSize, firstFieldCord, dir, board);
		}

	}
}
