using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SongGuesser.Interfaces;
using SongGuesser.Models;

namespace SongGuesser.Services
{
    public class GameService : IGameService
    {
        private readonly IPlaylistService _playlistService;

        public GameService(IPlaylistService playlistService)
        {
            _playlistService = playlistService;
        }

        public async Task<List<Track>> CreateGame()
        {
            var playlist = await _playlistService.GetPlaylist(923312155); // 80's hits for now 😉

            var tracks = PickRandomTracks(playlist.Tracks);

            return tracks;
        }


        // Gets a list of 10 tracks from a larger list
        private List<Track> PickRandomTracks(List<Track> tracks)
        {
            var randomTracks = new List<Track>();
            var rand = new Random();

            while(randomTracks.Count < 10)
            {
                var index = rand.Next(tracks.Count);
                if(!randomTracks.Contains(tracks[index]))
                    randomTracks.Add(tracks[index]);
            }

            return randomTracks;
        }
    }
}