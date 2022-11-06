using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPicturesController : ControllerBase
    {
        IUserPictureService _userPictureService;

        public UserPicturesController(IUserPictureService userPictureService)
        {
            _userPictureService = userPictureService;
        }

        [HttpPost("add")]
        public IActionResult Add(IFormFile file,[FromForm(Name =("Id"))] UserPicture userPicture)
        {
            var result = _userPictureService.add(file, userPicture);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
        [HttpPost("delete")]
        public IActionResult Delete([FromForm(Name = ("Id"))] int Id)
        {
            var entity = _userPictureService.GetById(Id);
            var result2 = _userPictureService.delete(entity.Data);
            if (result2.Success)
            {
                return Ok(result2.Message);
            }
            return BadRequest(result2.Message);
        }
        [HttpPost("update")]
        public IActionResult Update([FromForm(Name = ("UserPicture"))] IFormFile file, [FromForm(Name =("Id"))] int Id)
        {
            var entity = _userPictureService.GetById(Id);
            var result2 = _userPictureService.delete(entity.Data);
            var result = _userPictureService.update(file, entity.Data);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getbyuserid")]
        public IActionResult GetByUserId(int userId)
        {
            var result = _userPictureService.GetByUserId(userId);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
    }
}
