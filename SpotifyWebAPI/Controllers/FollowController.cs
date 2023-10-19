using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SpotifyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowController : ControllerBase
    {
        private IFollowService _followService;

        public FollowController(IFollowService followService)
        {
            _followService = followService;
        }


        [HttpPost("FollowIt")]
        public IActionResult FollowIt(int id)
        {
            var result=_followService.FollowIT(id);
            if (result!=null)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }


        [HttpPost("UnFollow")]
        public IActionResult UnFollow(int id)
        {
            var result = _followService.UnFollow(id);
            if (result != null)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }


        [HttpGet("GetMyTakipEttiklerim")]
        public IActionResult GetMyTakipEttiklerim()
        {
            var result = _followService.GetMyTakipEttiklerim().Data;
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpGet("GetMyTakipEdenler")]
        public IActionResult GetMyTakipEdenler()
        {
            var result = _followService.GetMyTakipEdenler().Data;
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
