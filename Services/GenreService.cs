using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SongGuesser.Interfaces;
using SongGuesser.Models;

namespace SongGuesser.Services
{
    public class GenreService : IGenreService
    {
        private readonly string baseUri;
        private readonly IHttpClientFactory _clientFactory;

        public GenreService(IConfiguration config, IHttpClientFactory clientFactory)
        {
            baseUri = config["Providers:Deezer:BaseUri"];
            _clientFactory = clientFactory;
        }

        public async Task<List<Genre>> GetAllGenres()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{baseUri}/genre");
            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if(response.IsSuccessStatusCode)
            {
                var genreContent = await response.Content.ReadAsStringAsync();
                var data = JsonDocument.Parse(genreContent).RootElement.GetProperty("data").ToString();
                var genres = JsonSerializer.Deserialize<List<Genre>>(data);

                return genres;
            }

            return new List<Genre>();
        }

        public async Task<Genre> GetGenre(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{baseUri}/genre/{id}");
            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if(response.IsSuccessStatusCode)
            {
                var genreContent = await response.Content.ReadAsStringAsync();
                var genre = JsonSerializer.Deserialize<Genre>(genreContent);

                return genre;
            }

            return new Genre();
        }
    }
}