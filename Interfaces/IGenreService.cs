using System.Collections.Generic;
using System.Threading.Tasks;
using SongGuesser.Models;

namespace SongGuesser.Interfaces
{
    public interface IGenreService
    {
        Task<List<Genre>> GetAllGenres();
        Task<Genre> GetGenre(int id);
    }
}