using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SongGuesser.Interfaces;
using SongGuesser.Models;

namespace SongGuesser.Controllers
{
    [ApiController]
    [Route("game")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        [Route("new")]
        public async Task<List<Track>> NewGame()
        {
            return await _gameService.CreateGame();
        }
    }
}