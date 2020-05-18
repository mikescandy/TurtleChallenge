using TurtleChallenge.App;
using TurtleChallenge.App.Enums;
using TurtleChallenge.App.Models;
using Xunit;

namespace TurtleChallenge.Test
{
    public class TurtleTests
    {
        [Fact]
        public void TurtleNorthRotateRightTest()
        {
            var turtle = new Turtle(Direction.North);
            turtle.RotateRight();
            Assert.Equal(Direction.East, turtle.Direction);
        }

        [Fact]
        public void TurtleEastRotateRightTest()
        {
            var turtle = new Turtle(Direction.East);
            turtle.RotateRight();
            Assert.Equal(Direction.South, turtle.Direction);
        }

        [Fact]
        public void TurtleSouthRotateRightTest()
        {
            var turtle = new Turtle(Direction.South);
            turtle.RotateRight();
            Assert.Equal(Direction.West, turtle.Direction);
        }

        [Fact]
        public void TurtleWestRotateRightTest()
        {
            var turtle = new Turtle(Direction.West);
            turtle.RotateRight();
            Assert.Equal(Direction.North, turtle.Direction);
        }
    }
}
