using System;
using TurtleChallenge.App.Enums;

namespace TurtleChallenge.App.Models
{
    public interface ITurtle
    {
        event EventHandler<TurtleMovementArgs> TurtleMoved;

        Direction Direction { get; }

        void MoveForward();

        void RotateRight();
    }
}