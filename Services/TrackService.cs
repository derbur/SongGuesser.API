using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks;
using SongGuesser.Interfaces;
using SongGuesser.Models;

namespace SongGuesser.Services
{
    public class TrackService : ITrackService
    {
        private readonly IHttpClientFactory _clientFactory;
        public TrackService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<Track> GetTrack(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.deezer.com/track/{id}");
            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if(response.IsSuccessStatusCode)
            {
                var apiTrack = await response.Content.ReadAsStringAsync();
                var track = JsonSerializer.Deserialize<Track>(apiTrack);
                return track;
            }
            return new Track();
        }
    }
}