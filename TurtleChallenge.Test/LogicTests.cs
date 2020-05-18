using Newtonsoft.Json;
using TurtleChallenge.App;
using TurtleChallenge.App.Enums;
using TurtleChallenge.App.Models;
using Xunit;

namespace TurtleChallenge.Test
{
    public class LogicTests
    {
        [Fact]
        public void TurtleMoveForwardSafe()
        {
            var initResult = Init();
            initResult.Turtle.RotateRight();
            initResult.Turtle.RotateRight();
            initResult.Turtle.MoveForward();
            Assert.Equal(Status.StillInDanger, initResult.Logic.Status);
        }

        [Fact]
        public void TurtleMoveForwardMine()
        {
            var initResult = Init();
            initResult.Turtle.MoveForward();
            Assert.Equal(Status.OnAMine, initResult.Logic.Status);
        }

        [Fact]
        public void TurtleMoveForwardExit()
        {
            var initResult = Init();
            initResult.Turtle.RotateRight();
            initResult.Turtle.MoveForward();
            Assert.Equal(Status.Exit, initResult.Logic.Status);
        }

        [Fact]
        public void TurtleMoveForwardOutOfBounds()
        {
            var initResult = Init();
            initResult.Turtle.RotateRight();
            initResult.Turtle.RotateRight();
            initResult.Turtle.MoveForward();
            initResult.Turtle.MoveForward();
            Assert.Equal(Status.OutOfBounds, initResult.Logic.Status);
        }

        private static (Logic Logic, ITurtle Turtle) Init()
        {
            //     |    |
            //     |  M | 2,2
            // ___ | ___| ___
            //     |    |  
            //     |  S |  E 
            // ___ | ___| ___
            //     |    |
            // 0,0 |    | 2,0
            //     |    |

            string json = @"{
                              'board': {
                                'width': 3,
                                'height': 3,
                                'mines':[
                                    { 'x':1, 'y':2}
                                    ]
                              },
                                'startTile':{'x':1,'y':1},
                                'exitTile':{'x':2, 'y':1},
                                'direction':'North'
                            }";

            var settings = JsonConvert.DeserializeObject<Settings>(json);
            var turtleFactory = new TurtleFactory(settings);
            var turtle = turtleFactory.CreateTurtle();
            var logic = new Logic(settings, turtle);
            return (logic, turtle);

        }
    }
}
