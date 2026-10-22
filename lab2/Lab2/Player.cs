using System;

namespace CatAndMouse
{
    public class Player
    {
        public string Name;
        public int Location = -1;
        public int DistanceTraveled = 0;
        private int _boardSize;

        public Player(string name, int boardSize)
        {
            Name = name;
            _boardSize = boardSize;
        }

        public void Move(int steps)
        {
            if (Location == -1)
            {
                Location = Wrap(steps);
            }
            else
            {
                DistanceTraveled += Math.Abs(steps);
                Location = Wrap(Location + steps);
            }
        }

        private int Wrap(int cell)
        {
            return ((cell - 1) % _boardSize + _boardSize) % _boardSize + 1;
        }
    }
}