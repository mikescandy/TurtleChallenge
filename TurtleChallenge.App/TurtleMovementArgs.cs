using System;
using TurtleChallenge.App.Enums;

namespace TurtleChallenge.App
{
    public class TurtleMovementArgs : EventArgs
    {
        public Movement Movement { get; }

        public TurtleMovementArgs(Movement direction)
        {
            Movement = direction;
        }
    }
}