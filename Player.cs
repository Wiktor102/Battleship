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

        public Dictionary<int, bool[]> ShipsSetUp;
        //public Dictionary<int, int> ShipsRemaning;

        public List<Ship> Ships = new List<Ship>();

        public Player(string name) {
            this.Name = name;
            ResetBoards();
        }

        public ShootResult Shoot(Board enemyBoard) {
            int i = 0;
            Cord cord;
            bool correctCord = true;

            do {
                if (i > 0) {
                    IO.DisplayError("Oddałeś już strzał na wybrane pole. Wybierz ponownie.");
                }

                cord = Cord.PromptForCord();
                if (enemyBoard.status[cord.y, cord.x].IsHit) correctCord = false;

                i++;
            } while (!correctCord);

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

            ShipsSetUp = new Dictionary<int, bool[]> {
                //[1] = new bool[]{ false, false, false, false },
                //[2] = new bool[]{ false, false, false },
                //[3] = new bool[]{ false, false },
                //[4] = new bool[] { false },
                [1] = new bool[] { false }
            };

            //ShipsRemaning = new Dictionary<int, int> {
            //    [1] = 4,
            //    [2] = 3,
            //    [3] = 2,
            //    [4] = 1,
            //};
        }
    }
}
