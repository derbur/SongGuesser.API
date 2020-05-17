using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SongGuesser.Interfaces;
using SongGuesser.Models;

namespace SongGuesser.Services
{
    public class ChartService : IChartService
    {
        private readonly string baseUri;
        private readonly IHttpClientFactory _clientFactory;

        public ChartService(IConfiguration config, IHttpClientFactory clientFactory)
        {
            baseUri = config["Providers:Deezer:BaseUri"];
            _clientFactory = clientFactory;
        }

        public async Task<Chart> GetChart(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{baseUri}/chart/{id}/tracks");
            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if(response.IsSuccessStatusCode)
            {
                var chartContent = await response.Content.ReadAsStringAsync();

                // Parse the data array, because for some reason deezer returns this inside a data property
                var data = JsonDocument.Parse(chartContent).RootElement.GetProperty("data").ToString();

                var chart = new Chart{ Tracks = JsonSerializer.Deserialize<List<Track>>(data) };

                return chart;
            }

            return new Chart();
        }
    }
}