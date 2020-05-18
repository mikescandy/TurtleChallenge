using System;
using TurtleChallenge.App.Enums;

namespace TurtleChallenge.App.Models
{
    public class Turtle : ITurtle
    {
        public event EventHandler<TurtleMovementArgs> TurtleMoved;

        public Direction Direction { get; private set; }

        public Turtle(Direction direction)
        {
            Direction = direction;
        }

        public void MoveForward()
        {
            TurtleMoved?.Invoke(this, new TurtleMovementArgs(Movement.Forward));
        }

        public void MoveBack()
        {
            TurtleMoved?.Invoke(this, new TurtleMovementArgs(Movement.Backward));

        }

        public void RotateLeft()
        {
            Direction = (Direction)((((int)Direction) + 3) % 4);
        }

        public void RotateRight()
        {
            Direction = (Direction)((((int)Direction) + 1) % 4);
        }
    }

}