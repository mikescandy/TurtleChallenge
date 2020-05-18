using System.Collections.Generic;
using Newtonsoft.Json;

namespace TurtleChallenge.App.Models
{
    public partial class Board
    {
        [JsonProperty("width")]
        public long Width { get; set; }

        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("mines")]
        public List<Tile> Mines { get; set; }
    }
}