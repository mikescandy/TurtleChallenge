using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TurtleChallenge.App.Enums;

namespace TurtleChallenge.App.Models
{
    public partial class Settings : ITurtleSettings, IBoardSettings
    {
        [JsonProperty("board")]
        public Board Board { get; set; }

        [JsonProperty("startTile")]
        public Tile StartTile { get; set; }

        [JsonProperty("exitTile")]
        public Tile ExitTile { get; set; }

        [JsonProperty("direction")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Direction Direction { get; set; }
    }
}