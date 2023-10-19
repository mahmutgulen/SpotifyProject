using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace SpotifyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private IFavoriteService _favoriteService;

        public FavoriteController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        [HttpGet("GetMyFavorites")]
        public IActionResult GetMyFavorites()
        {
            var result = _favoriteService.GetMyFavorites().Data;
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPost("AddToMyFavorites")]
        public IActionResult AddToMyFavorites(int id)
        {
            var result = _favoriteService.AddToMyFavorites(id);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("DeleteOnMyFavorites")]
        public IActionResult DeleteOnMyFavorites(int songId)
        {
            var result = _favoriteService.DeleteOnMyFavorites(songId);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }




    }
}
