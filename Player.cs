using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    enum ShootSuccess {  FullSuccess, Success, Failure }
    internal class Player
    {
        public string Name;
        public int WonGames;

        public Board board;

        public Dictionary<int, bool[]> ShipsSetUp;
        public Dictionary<int, int> ShipsRemaning;

        public List<Ship> Ships = new List<Ship>();

        public Player(string name)
        {
            this.Name = name;
            ResetBoards();
        }

        public ShootSuccess Shoot(Board enemyBoard) {
            int i = 0;
            Cord cord;
            bool correct = true;
            do
            {
                if (i > 0)
                {
                    IO.DisplayError("Oddałeś już strzał na wybrane pole. Wybierz ponownie.");
                }

                cord = Cord.PromptForCord();
                if (enemyBoard.status[cord.y, cord.x].IsHit) correct = false;
                
                i++;
            } while (!correct);

            enemyBoard.status[cord.y, cord.x].IsHit = true;
            var hitCell = enemyBoard.status[cord.y, cord.x];


            if (hitCell is ShipBoardCell)
            {
                var tmp = (ShipBoardCell)hitCell;
                return tmp.IsSunken ? ShootSuccess.FullSuccess: ShootSuccess.Success;
            }

            return ShootSuccess.Failure;
        }

        public void ResetBoards()
        {
            board = new Board();

            ShipsSetUp = new Dictionary<int, bool[]> {
                //[1] = new bool[]{ false, false, false, false },
                //[2] = new bool[]{ false, false, false },
                //[3] = new bool[]{ false, false },
                [4] = new bool[]{ false },
            };

            ShipsRemaning = new Dictionary<int, int>
            {
                [1] = 4,
                [2] = 3,
                [3] = 2,
                [4] = 1,
            };
        }
    }
}
