using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace SpotifyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayListController : ControllerBase
    {
        private IPlayListService _playListService;

        public PlayListController(IPlayListService playListService)
        {
            _playListService = playListService;
        }

        [HttpPost("AddPlaylist")]
        public IActionResult AddPlayList(string name)
        {
            var result = _playListService.AddPlayList(name);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("AddSongInPlayList")]
        public IActionResult AddSongInPlayList(int pl_id, int songId)
        {
            var result = _playListService.AddSongInPlayList(pl_id, songId);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }


        [HttpPost("DeletePlayList")]
        public IActionResult DeletePlayList(int pl_id)
        {
            var result = _playListService.DeletePlaylist(pl_id);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("DeleteSongInPlayList")]
        public IActionResult DeleteSongInPlayList(int pl_id,int songId)
        {
            var result = _playListService.DeleteSongInPlayList(pl_id,songId);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
