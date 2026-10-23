using System;
using System.IO;

namespace CatAndMouse
{
    public enum GameState { Start, End }

    public class Game
    {
        public static string InputFile = "1.ChaseData.txt";
        public static string OutputFile = "PursuitLog.txt";

        private int _size;
        private readonly Player _cat;
        private readonly Player _mouse;
        private GameState _state;

        public Game() : this(0) { }

        public Game(int size)
        {
            _size = size;
            _cat = new Player("Cat", size);
            _mouse = new Player("Mouse", size);
            _state = GameState.Start;
        }

        public void Run()
        {
            using (StreamReader reader = new StreamReader(InputFile))
            using (StreamWriter writer = new StreamWriter(OutputFile))
            {
                ReadBoardSize(reader);
                WriteHeader(writer);
                ProcessCommands(reader, writer);
                WriteSummary(writer);
            }
        }

        private void ReadBoardSize(StreamReader reader)
        {
            string line = reader.ReadLine();
            int size;
            if (line != null && int.TryParse(line.Trim(), out size) && size > 0)
            {
                _size = size;
                _cat.SetBoardSize(size);
                _mouse.SetBoardSize(size);
            }
        }

        private void ProcessCommands(StreamReader reader, StreamWriter writer)
        {
            string line;
            while (_state != GameState.End && (line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 0) continue;

                char command = parts[0][0];
                if (command == 'P')
                {
                    DoPrintCommand(writer);
                }
                else
                {
                    int steps = int.Parse(parts[1]);
                    if (command == 'M') _mouse.Move(steps);
                    else if (command == 'C') _cat.Move(steps);

                    if (_cat.IsInGame && _mouse.IsInGame && _cat.Location == _mouse.Location)
                    {
                        _cat.MarkWinner();
                        _mouse.MarkLoser();
                        _state = GameState.End;
                    }
                }
            }

            if (_state != GameState.End)
            {
                _cat.MarkLoser();
                _mouse.MarkWinner();
                _state = GameState.End;
            }
        }

        private void WriteHeader(StreamWriter writer)
        {
            writer.WriteLine("Cat and Mouse");
            writer.WriteLine();
            writer.WriteLine("Cat Mouse  Distance");
            writer.WriteLine(new string('-', 19));
        }

        private void DoPrintCommand(StreamWriter writer)
        {
            string catCol = " " + (_cat.IsInGame ? _cat.Location.ToString() : "??").PadLeft(2);
            string mouseCol = (_mouse.IsInGame ? _mouse.Location.ToString() : "??").PadLeft(2);
            string row = catCol + "    " + mouseCol;
            
            if (_cat.IsInGame && _mouse.IsInGame)
            {
                int dist = Math.Abs(_cat.Location - _mouse.Location);
                row += dist.ToString().PadLeft(10);
            }
            writer.WriteLine(row);
        }

        private void WriteSummary(StreamWriter writer)
        {
            writer.WriteLine(new string('-', 19));
            writer.WriteLine();
            writer.WriteLine();
            writer.WriteLine("Distance traveled:   Mouse    Cat");
            writer.WriteLine(new string(' ', 21) + _mouse.DistanceTraveled.ToString().PadLeft(5)
                + new string(' ', 4) + _cat.DistanceTraveled.ToString().PadLeft(3));
            writer.WriteLine();

            if (_cat.State == State.Winner)
                writer.WriteLine("Mouse caught at: " + _mouse.Location);
            else
                writer.WriteLine("Mouse evaded Cat");
            
        }
    }
}