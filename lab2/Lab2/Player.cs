namespace CatAndMouse
{
    public class Player
    {
        public string Name;
        public int Location;
        public int DistanceTraveled;

        public Player(string name)
        {
            Name = name;
            Location = -1; // not in game
            DistanceTraveled = 0;
        }

        public void Move(int steps)
        {
            //make normal field calculation
            Location += steps; 
        }
    }
}