using System.Threading.Tasks;
using SongGuesser.Models;

namespace SongGuesser.Interfaces
{
    public interface IChartService
    {
        Task<Chart> GetChart(int id);
    }
}