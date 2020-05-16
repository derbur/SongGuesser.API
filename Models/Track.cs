using System.Text.Json.Serialization;

namespace SongGuesser.Models
{
    public class Track {
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("preview")]
        public string Preview { get; set; }
    }
}