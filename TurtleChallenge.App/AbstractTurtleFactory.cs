using TurtleChallenge.App.Models;

namespace TurtleChallenge.App
{
    public abstract class AbstractTurtleFactory
    {
        public abstract ITurtle CreateTurtle();
    }
}