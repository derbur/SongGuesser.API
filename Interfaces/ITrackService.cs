using System.Threading.Tasks;
using SongGuesser.Models;

namespace SongGuesser.Interfaces
{
    public interface ITrackService
    {
        Task<Track> GetTrack(int id);
    }
}