using FluentValidation.Validators;
using System.Collections.Generic;
using System.Linq;
using TurtleChallenge.App.Models;

namespace TurtleChallenge.App.Validators
{
    public class TilesMustBeWithinBoard : PropertyValidator
    {
        public TilesMustBeWithinBoard()
            : base("Property {PropertyName} contains children born before their parent!")
        {
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var board = context.ParentContext.InstanceToValidate as Board;
            var tiles = context.PropertyValue as IList<Tile>;

            if (tiles != null)
            {
                return !(tiles.Any(t => t.X < 0 || t.X >= board.Width || t.Y < 0 || t.Y >= board.Height));
            }

            return true;
        }
    }
}
