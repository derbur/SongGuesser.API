using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SongGuesser.Interfaces;
using SongGuesser.Models;

namespace SongGuesser.Controllers
{
    [ApiController]
    [Route("chart")]
    public class ChartController
    {
        private readonly IChartService _chartService;

        public ChartController(IChartService chartService)
        {
            _chartService = chartService;            
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Chart> Get([FromRoute] int id)
        {
            var chart = await _chartService.GetChart(id);
            return chart;
        }
    }
}