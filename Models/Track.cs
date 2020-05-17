using System.Text.Json.Serialization;

namespace SongGuesser.Models
{
    public class Track {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("title_short")]
        public string TitleShort { get; set; }
        [JsonPropertyName("preview")]
        public string Preview { get; set; }
        [JsonPropertyName("artist")]
        public Artist Artist { get; set; }
    }
}