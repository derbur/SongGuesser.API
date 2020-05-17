using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SongGuesser.Interfaces;
using SongGuesser.Models;

namespace SongGuesser.Services
{
    public class PlaylistService : IPlaylistService
    {
        private readonly string baseUri;
        private readonly IHttpClientFactory _clientFactory;

        public PlaylistService(IConfiguration config, IHttpClientFactory clientFactory)
        {
            baseUri = config["Providers:Deezer:BaseUri"];
            _clientFactory = clientFactory;
        }

        public async Task<Playlist> GetPlaylist(int id)
        {
            // limit needs to be taken out of query string and instead use the next parameter to iterate through pages.
            var request = new HttpRequestMessage(HttpMethod.Get, $"{baseUri}/playlist/{id}/tracks?limit=100");
            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if(response.IsSuccessStatusCode)
            {
                var playlistContent = await response.Content.ReadAsStringAsync();

                // Parse the data array, because for some reason deezer returns this inside a data property
                var data = JsonDocument.Parse(playlistContent).RootElement.GetProperty("data").ToString();

                var playlist = new Playlist { Tracks = JsonSerializer.Deserialize<List<Track>>(data) };

                return playlist;
            }

            return new Playlist();
        }
    }
}