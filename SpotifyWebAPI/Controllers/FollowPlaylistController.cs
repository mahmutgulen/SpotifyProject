using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SpotifyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowPlaylistController : ControllerBase
    {
        private IPlayListsIFollowService _followService;

        public FollowPlaylistController(IPlayListsIFollowService followService)
        {
            _followService = followService;
        }

        [HttpPost("AddFollowPlaylist")]
        public IActionResult AddFollowPlaylist(int id)
        {
            var result = _followService.AddFollowPlaylist(id);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }



        [HttpPost("DeleteFollowPlaylist")]
        public IActionResult DeleteOnMyFavorites(int Pl_Id)
        {
            var result = _followService.DeleteFollowPlaylist(Pl_Id);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }


        [HttpGet("GetMyFollows")]
        public IActionResult GetMyFollows()
        {
            var result = _followService.GetMyFollows().Data;
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
