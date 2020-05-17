using System.Threading.Tasks;
using SongGuesser.Models;

namespace SongGuesser.Interfaces
{
    public interface IPlaylistService
    {
        Task<Playlist> GetPlaylist(int id);
    }
}