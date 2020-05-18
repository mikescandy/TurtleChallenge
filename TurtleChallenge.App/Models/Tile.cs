using Newtonsoft.Json;
using System;

namespace TurtleChallenge.App.Models
{
    public sealed partial class Tile : IEquatable<Tile>
    {
        public Tile()
        {
        }

        public Tile(Tile tile)
        {
            X = tile.X;
            Y = tile.Y;
        }

        [JsonProperty("x")]
        public long X { get; set; }

        [JsonProperty("y")]
        public long Y { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Tile);
        }

        public bool Equals(Tile p)
        {
            // If parameter is null, return false.
            if (p is null)
            {
                return false;
            }

            // Optimization for a common success case.
            if (ReferenceEquals(this, p))
            {
                return true;
            }

            // If run-time types are not exactly the same, return false.
            if (GetType() != p.GetType())
            {
                return false;
            }

            // Return true if the fields match.
            // Note that the base class is not invoked because it is
            // System.Object, which defines Equals as reference equality.
            return (X == p.X) && (Y == p.Y);
        }

        public override int GetHashCode()
        {
            return (int)(X * 0x00010000 + Y);
        }

        public static bool operator ==(Tile lhs, Tile rhs)
        {
            // Check for null on left side.
            if (lhs is null)
            {
                if (rhs is null)
                {
                    // null == null = true.
                    return true;
                }

                // Only the left side is null.
                return false;
            }
            // Equals handles case of null on right side.
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Tile lhs, Tile rhs)
        {
            return !(lhs == rhs);
        }
    }
}