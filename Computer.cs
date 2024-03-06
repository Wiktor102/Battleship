using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship {
	internal class Computer : Player{
		private Dictionary<string, List<Cord>> _potentialCords;

		public Computer() : base("Komputer") { }

		public override void SetUpShips(DisplayFunc displayCallback) {
			foreach (KeyValuePair<int, int> entry in Settings.ShipsConfiguration) {
				var shipLength = entry.Key;

				for (int i = 0; i < entry.Value; i++) {
					Random rand = new Random();
					Direction dir;
					Cord cord;
					do {
						dir = rand.Next(0, 2) == 0 ? Direction.Horizontal : Direction.Vertical;
						cord = Cord.RandomCord();
					} while (!board.ValidateShipPlacement(cord, shipLength, dir));

					Ships.Add(new Ship(shipLength, cord, dir, board));
				}
			}
		}

		public override ShootResult Shoot(Board enemyBoard) {
			Cord cord = null;
			BoardCell cell = null;
			Random random = new Random();
			string usedPotentialDirection = null;
			int i = 0;

			do {
				if (_potentialCords == null || i > 0) { // Jeśli z jakiegoś powodu jesteśmy w 2 lub kolejnej iteracji to zawsze losowe pole
					// For debug:
					//if (i > 0) cord = new Cord(random.Next(0, 10), random.Next(0, 10));
					//else cord = new Cord(1, 1);


					cord = new Cord(random.Next(0, 10), random.Next(0, 10));
					cell = enemyBoard.status[cord.y, cord.x];
					continue;
				}


                foreach (var potentialCordList in new Dictionary<string, List<Cord>>(_potentialCords)) // Ponieważ wewnątrz pętli nie można modyfikować kolekcji to iteruję przez jej płytką kopię
                {
					if (cord != null) break;

					if (potentialCordList.Value.Count == 0) { 
						_potentialCords.Remove(potentialCordList.Key);
						continue;
					}

                    foreach (var potentialCord in potentialCordList.Value.ToList())
                    {
						cord = potentialCord;
						cell = enemyBoard.status[cord.y, cord.x];
						usedPotentialDirection = potentialCordList.Key;
						potentialCordList.Value.Remove(potentialCord);
						goto while_loop_end; // Niestety w c# nie ma innej możliwości przerwania pętli-rodzica
					}
                }

				while_loop_end:
				i++;
            } while (cell.IsHit || (cell is EmptyBoardCell && ((EmptyBoardCell)cell).IsBlocked));

			enemyBoard.status[cord.y, cord.x].Hit();
			var hitCell = enemyBoard.status[cord.y, cord.x];

			if (hitCell is ShipBoardCell tmp) {
				if (tmp.ParentShip.IsSunken) {
					_potentialCords = null;
					enemyBoard.SurroundShip(tmp.ParentShip);
					return ShootResult.FullSuccess;
				}

				if (_potentialCords == null) _potentialCords = GetPotentialCords(cord);
				return ShootResult.Success;
			}

			if (usedPotentialDirection != null) _potentialCords.Remove(usedPotentialDirection);
			return ShootResult.Failure;
		}

		private Dictionary<string, List<Cord>> GetPotentialCords(Cord cord) {
			var dict = new Dictionary<string, List<Cord>> {
				["left"] = new List<Cord>(),
				["right"] = new List<Cord>(),
				["top"] = new List<Cord>(),
				["bottom"] = new List<Cord>(),
			};

			// TODO: sprawdzać liczbę już zatopionych statków żeby ograniczyć liczbę potencjalnych pól
			for (int i = 1; i < 4; i++) {
				if (cord.x - i >= 0) dict["left"].Add(new Cord(cord.x - i, cord.y));
				if (cord.x + i < 10) dict["right"].Add(new Cord(cord.x + i, cord.y));
				if (cord.y - i >= 0) dict["top"].Add(new Cord(cord.x, cord.y - i));
				if (cord.y + i < 10) dict["bottom"].Add(new Cord(cord.x, cord.y + i));
			}

			return dict;
		}
	}
}
