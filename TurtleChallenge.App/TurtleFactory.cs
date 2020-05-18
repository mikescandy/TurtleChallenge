using TurtleChallenge.App.Models;

namespace TurtleChallenge.App
{
    public class TurtleFactory : AbstractTurtleFactory
    {
        private readonly ITurtleSettings _turtleSettings;

        public TurtleFactory(ITurtleSettings turtleSettings)
        {
            _turtleSettings = turtleSettings;
        }

        public override ITurtle CreateTurtle()
        {
            return new Turtle(_turtleSettings.Direction);
        }
    }
}