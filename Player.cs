using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    internal class Player
    {
        public string Name;
        public int WonGames;

        public OwnBoard MyBoard;
        public EnemyBoard NotMyBoard;

        public Dictionary<int, bool[]> ShipsSetUp;
        public Dictionary<int, int> ShipsRemaning;

        public Player(string name)
        {
            this.Name = name;
            ResetBoards();
        }

        public void ResetBoards()
        {
            MyBoard = new OwnBoard();
            NotMyBoard = new EnemyBoard();

            ShipsSetUp = new Dictionary<int, bool[]> {
                [1] = new bool[]{ false, false, false, false },
                [2] = new bool[]{ false, false, false },
                [3] = new bool[]{ false, false },
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

        public void DisplayNonSetUpShips()
        {
            foreach (KeyValuePair<int, bool[]> entry in ShipsSetUp)
            {
                // Count 
            }
        }
    }
}
