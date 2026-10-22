using System;
using System.IO;

namespace CatAndMouse
{
    public class Game
    {
        public static string InputFile = "1.ChaseData.txt";
        public static string OutputFile = "PursuitLog.txt";

        private int _size;
        public Player cat;
        public Player mouse;

        public Game(int size)
        {
            _size = size;
            cat = new Player("Cat", size);
            mouse = new Player("Mouse", size);
        }

        public void Run()
        {
            using (StreamReader reader = new StreamReader(InputFile))
            {
                string line = reader.ReadLine();
                if (line != null) _size = int.Parse(line.Trim());
                
                // TODO: Дописать полноценный процессор команд и запись в PursuitLog
                Console.WriteLine("Board size parsed: " + _size);
            }
        }
    }
}