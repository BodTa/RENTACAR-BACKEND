using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        IFavoriteService _favoriteService;

        public FavoritesController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }
        [HttpGet("getbyuserid")]
        public IActionResult GetByUserId(int userId)
        {
            var result = _favoriteService.GetByUserId(userId);
            return Ok(result.Data); 
        }
        [HttpPost("add")]
        public IActionResult Add(Favorite favorite)
        {
            var result = _favoriteService.add(favorite);
            return Ok(result);
        }
        [HttpPost("delete")]
        public IActionResult Delete(Favorite favorite)
        {
            var fav = _favoriteService.GetByCarIdAndUserId(favorite.CarId,favorite.UserId);
            var result = _favoriteService.delete(fav.Data);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
