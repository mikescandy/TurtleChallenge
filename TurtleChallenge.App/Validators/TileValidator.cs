using FluentValidation;
using TurtleChallenge.App.Models;

namespace TurtleChallenge.App.Validators
{
    public class TileValidator : AbstractValidator<Tile>
    {
        public TileValidator()
        {
            RuleFor(t => t.X).GreaterThanOrEqualTo(0);
            RuleFor(t => t.Y).GreaterThanOrEqualTo(0);
        }
    }
}
