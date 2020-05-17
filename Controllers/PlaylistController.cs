using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SongGuesser.Interfaces;
using SongGuesser.Models;

namespace SongGuesser.Controllers
{
    [ApiController]
    [Route("playlist")]
    public class PlaylistController : ControllerBase
    {
        private readonly IPlaylistService _playlistService;
        private readonly int topUsaPlaylist; // Temporary until we get the choice for more playlists on the frontend
        public PlaylistController(IConfiguration config, IPlaylistService playlistService)
        {
            topUsaPlaylist = Int32.Parse(config["Providers:Deezer:Playlists:TopUSA"]);
            _playlistService = playlistService;
        }

        [HttpGet]
        public Task<Playlist> Get()
        {
            var playlist = _playlistService.GetPlaylist(topUsaPlaylist);
            return playlist;
        }
    }
}