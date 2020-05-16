using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SongGuesser.Interfaces;
using SongGuesser.Models;

namespace SongGuesser.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrackController : ControllerBase
    {
        private static ITrackService _trackService;

        public TrackController(ITrackService trackService)
        {
            _trackService = trackService;
        }

        [HttpGet]
        public async Task<Track> Get()
        {
            var track = await _trackService.GetTrack(3135556);
            return track;
        }
    }
}
