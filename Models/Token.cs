namespace SongGuesser.Models
{
    public class Token
    {
        public Provider Provider { get; set; }
        public string Value { get; set; }
        public int Expiration { get; set; }
    }
}