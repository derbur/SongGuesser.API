using System.Collections.Generic;
using System.Threading.Tasks;
using SongGuesser.Models;

namespace SongGuesser.Interfaces
{
    public interface IGameService
    {
        Task<List<Track>> CreateGame();
    }
}