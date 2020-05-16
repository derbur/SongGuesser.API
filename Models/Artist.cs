using System.Text.Json.Serialization;

namespace SongGuesser.Models
{
    public class Artist
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}