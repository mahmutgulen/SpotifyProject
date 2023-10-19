using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SpotifyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private ILibraryService _libraryService;

        public LibraryController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }


        [HttpGet("GetLibrary")]
        public IActionResult GetLibrary()
        {
            var result = _libraryService.GetLibrary().Data;
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }


    }
}
