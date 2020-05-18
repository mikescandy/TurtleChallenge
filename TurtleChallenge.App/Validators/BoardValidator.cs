using FluentValidation;
using TurtleChallenge.App.Models;

namespace TurtleChallenge.App.Validators
{
    public class BoardValidator : AbstractValidator<Board>
    {
        public BoardValidator()
        {
            RuleFor(b => b.Height).GreaterThanOrEqualTo(0);
            RuleFor(b => b.Width).GreaterThanOrEqualTo(0);
            RuleFor(b => b.Mines).NotEmpty();
            RuleFor(b => b.Mines).SetValidator(new TilesMustBeWithinBoard());
            RuleForEach(b => b.Mines).SetValidator(new TileValidator());

        }
    }
}
