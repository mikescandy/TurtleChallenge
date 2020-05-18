using FluentValidation;
using System.Linq;
using TurtleChallenge.App.Models;

namespace TurtleChallenge.App.Validators
{

    public class SettingsValidator : FluentValidation.AbstractValidator<Settings>
    {
        public SettingsValidator()
        {
            RuleFor(x => x.Board).NotEmpty();
            RuleFor(x => x.Board).SetValidator(new BoardValidator());

            RuleFor(x => x.ExitTile).NotEmpty();
            RuleFor(x => x.StartTile).NotEmpty();
            RuleFor(x => x.ExitTile).Must((s, t) => t.X == 0 || t.X < s.Board.Width).When(x => x.Board != null).When(x => x.ExitTile != null).When(x => x.Board.Width > 0);
            RuleFor(x => x.ExitTile).Must((s, t) => t.Y == 0 || t.Y < s.Board.Height).When(x => x.Board != null).When(x => x.ExitTile != null).When(x => x.Board.Height > 0);
            RuleFor(x => x.StartTile).Must((s, t) => t.X == 0 || t.X < s.Board.Width).When(x => x.Board != null).When(x => x.StartTile != null).When(x => x.Board.Width > 0);
            RuleFor(x => x.StartTile).Must((s, t) => t.Y == 0 || t.Y < s.Board.Height).When(x => x.Board != null).When(x => x.StartTile != null).When(x => x.Board.Height > 0);
            RuleFor(x => x.StartTile).Must((s, t) => s.Board.Mines.All(m => m != t)).When(x => x.Board != null).When(x => x.Board.Mines != null);
            RuleFor(x => x.ExitTile).Must((s, t) => s.Board.Mines.All(m => m != t)).When(x => x.Board != null).When(x => x.Board.Mines != null);
        }
    }
}
