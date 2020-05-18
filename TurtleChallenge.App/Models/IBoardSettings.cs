namespace TurtleChallenge.App.Models
{
    public interface IBoardSettings
    {
        Board Board { get; set; }
        Tile ExitTile { get; set; }
        Tile StartTile { get; set; }

    }
}