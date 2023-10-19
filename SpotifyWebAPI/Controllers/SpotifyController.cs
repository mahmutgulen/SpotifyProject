using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace SpotifyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpotifyController : ControllerBase
    {
        private ISpotifyService _spotifyService;

        public SpotifyController(ISpotifyService spotifyService)
        {
            _spotifyService = spotifyService;
        }

        [HttpGet("getSpotifyToken")]
        public IActionResult GetSpotifyToken()
        {
            var result = _spotifyService.GetSpotifyToken();
            if (result != null)
            {
                return Ok(result.Message);
            }
            return BadRequest();
        }

        [HttpGet("getByIdInSpotify")]
        public IActionResult GetByIdInSpotify(string id)
        {
            var result = _spotifyService.GetByIdInSpotify(id);
            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPost("AddToSongRepository")]
        public IActionResult AddToSongRepository(string id)
        {
            var result = _spotifyService.AddToSongRepository(id);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("AddAlbum")]
        public IActionResult AddAlbum(string id)
        {
            var result = _spotifyService.AddToAlbum(id);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("GetSongRepositroy")]
        public IActionResult GetSongRepositroy()
        {
            var result=_spotifyService.GetSongRepository();
            if (result!=null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

    }
}
