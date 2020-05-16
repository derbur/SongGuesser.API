using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SongGuesser.Interfaces;
using SongGuesser.Models;

namespace SongGuesser.Services
{
    public class TrackService : ITrackService
    {
        private readonly string baseUri;
        private readonly IHttpClientFactory _clientFactory;
        
        public TrackService(IConfiguration config, IHttpClientFactory clientFactory)
        {
            baseUri = config["Providers:Deezer:BaseUri"];
            _clientFactory = clientFactory;
        }

        public async Task<Track> GetTrack(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{baseUri}/track/{id}");
            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if(response.IsSuccessStatusCode)
            {
                var trackContent = await response.Content.ReadAsStringAsync();
                var track = JsonSerializer.Deserialize<Track>(trackContent);
                return track;
            }
            return new Track();
        }
    }
}