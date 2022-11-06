using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRateController : ControllerBase
    {
        private IUserRateService _userRateService;

        public UserRateController(IUserRateService userRateService)
        {
            _userRateService = userRateService;
        }
        [HttpPost("add")]
        public IActionResult Add(UserRate userRate)
        {
            var result = _userRateService.add(userRate);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("delete")]
        public IActionResult Delete(UserRate userRate)
        {
            var result = _userRateService.delete(userRate);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("update")]
        public IActionResult Update(UserRate userRate)
        {
            var result = _userRateService.update(userRate);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
