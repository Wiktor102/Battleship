using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship {
    enum ShootResult { FullSuccess, Success, Failure }
    internal class Player {
        public string Name;
        public int WonGames;

        public Board board;
        public List<Ship> Ships = new List<Ship>();

        public Player(string name) {
            this.Name = name;
            ResetBoards();
        }

        public ShootResult Shoot(Board enemyBoard) {
            Cord cord;

            for (int i = 0;  true; i++) {
				if (i > 0) {
					IO.DisplayError("Oddałeś już strzał na wybrane pole. Wybierz ponownie:", true);
				}

				cord = Cord.PromptForCord();
                BoardCell cellAtCord = enemyBoard.status[cord.y, cord.x];
				if (!cellAtCord.IsHit && (cellAtCord is ShipBoardCell || !((EmptyBoardCell)cellAtCord).IsBlocked)) break; // Brake loop if cell hasn't been hit yet and isn't blocked
			}

            enemyBoard.status[cord.y, cord.x].Hit();
            var hitCell = enemyBoard.status[cord.y, cord.x];


            if (hitCell is ShipBoardCell tmp) {
                if (tmp.ParentShip.IsSunken) {
                    enemyBoard.SurroundShip(tmp.ParentShip);
                    return ShootResult.FullSuccess;
                }

                return ShootResult.Success;
            }

            return ShootResult.Failure;
        }

        public int GetTotalRamaningShips() {
            return GetRemaingShipsCountByCategory().Values.Sum();
        }

        public Dictionary<int, int> GetRemaingShipsCountByCategory() {
            var remaningShips = new Dictionary<int, int> { [1] = 0, [2] = 0, [3] = 0, [4] = 0};

            foreach (var ship in Ships)
            {
                if (!ship.IsSunken) remaningShips[ship.Size]++;
            }

            return remaningShips;
        }

        public void ResetBoards() {
            board = new Board();
        }
    }
}
