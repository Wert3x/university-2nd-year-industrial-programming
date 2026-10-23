using System;

namespace CatAndMouse
{
    public enum State { NotInGame, Playing, Winner, Loser }

    public class Player
    {
        private readonly string _name;
        private int _boardSize;
        private int _location;
        private State _state;
        private int _distanceTraveled;

        public Player(string name, int boardSize)
        {
            _name = name;
            _boardSize = boardSize;
            _location = 0;
            _state = State.NotInGame;
            _distanceTraveled = 0;
        }

        public string Name => _name;
        public int Location => _location;
        public State State => _state;
        public int DistanceTraveled => _distanceTraveled;
        public bool IsInGame => _state != State.NotInGame;

        public void SetBoardSize(int boardSize) => _boardSize = boardSize;

        public void Move(int steps)
        {
            if (_state == State.NotInGame)
            {
                _location = Wrap(steps);
            }
            else
            {
                _distanceTraveled += Math.Abs(steps);
                _location = Wrap(_location + steps);
            }
            _state = State.Playing;
        }

        public void MarkWinner() => _state = State.Winner;
        public void MarkLoser() => _state = State.Loser;

        private int Wrap(int cell)
        {
            return ((cell - 1) % _boardSize + _boardSize) % _boardSize + 1;
        }
    }
}