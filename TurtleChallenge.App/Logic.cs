using System.Diagnostics;
using System.Linq;
using TurtleChallenge.App.Enums;
using TurtleChallenge.App.Models;

namespace TurtleChallenge.App
{
    public class Logic
    {
        private readonly IBoardSettings _settings;
        private readonly ITurtle _turtle;
        private readonly Tile _turtlePosition;

        public Status Status { get; private set; }

        public Logic(IBoardSettings settings, ITurtle turtle)
        {
            _settings = settings;
            _turtle = turtle;
            _turtle.TurtleMoved += TurtleMoved;
            _turtlePosition = new Tile(_settings.StartTile);
        }

        private void TurtleMoved(object sender, TurtleMovementArgs e)
        {
            EvaluateMovement(e.Movement);
            EvaluateTurtlePosition();
        }

        private void EvaluateTurtlePosition()
        {
            Debug.WriteLine($"Current position: X:{_turtlePosition.X} Y:{_turtlePosition.Y}");

            if (_turtlePosition.X < 0 || _turtlePosition.X > _settings.Board.Width ||
                _turtlePosition.Y < 0 || _turtlePosition.Y > _settings.Board.Height)
            {
                Status = Status.OutOfBounds;
            }
            else if (_settings.Board.Mines.Any(mine => mine == _turtlePosition))
            {
                Status = Status.OnAMine;
            }
            else if (_turtlePosition == _settings.ExitTile)
            {
                Status = Status.Exit;
            }
            else
            {
                Status = Status.StillInDanger;
            }
        }

        private void EvaluateMovement(Movement movement)
        {
            switch (movement)
            {
                case Movement.Forward:
                    switch (_turtle.Direction)
                    {
                        case Direction.North:
                            _turtlePosition.Y++;
                            break;
                        case Direction.East:
                            _turtlePosition.X++;
                            break;
                        case Direction.South:
                            _turtlePosition.Y--;
                            break;
                        case Direction.West:
                            _turtlePosition.X--;
                            break;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}