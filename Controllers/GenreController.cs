using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SongGuesser.Interfaces;
using SongGuesser.Models;

namespace SongGuesser.Controllers
{
    [ApiController]
    [Route("genre")]
    public class GenreController
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;            
        }

        [HttpGet]
        [Route("/genres")]
        public async Task<List<Genre>> Get()
        {
            var genres = await _genreService.GetAllGenres();
            return genres;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Genre> Get([FromRoute] int id)
        {
            var genre = await _genreService.GetGenre(id);
            return genre;
        }
    }
}