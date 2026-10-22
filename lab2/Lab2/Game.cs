using System;
using System.IO;

namespace CatAndMouse
{
    public class Game
    {
        public static string InputFile = "1.ChaseData.txt";
        public static string OutputFile = "PursuitLog.txt";

        public int Size;
        public Player cat;
        public Player mouse;

        public Game(int size)
        {
            Size = size;
            cat = new Player("Cat");
            mouse = new Player("Mouse");
        }

        public void Run()
        {
            // just basics, need to realise reading files logics
            Console.WriteLine("Game started, size: " + Size);
        }
    }
}