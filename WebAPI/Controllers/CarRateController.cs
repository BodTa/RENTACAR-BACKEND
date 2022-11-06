using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarRateController : ControllerBase
    {
        private readonly ICarRateService _carRateService;

        public CarRateController(ICarRateService carRateService)
        {
            _carRateService = carRateService;
        }

        [HttpPost("add")]
        public IActionResult Add(CarRate carRate)
        {
            var result = _carRateService.add(carRate);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("delete")]
        public IActionResult Delete(CarRate carRate)
        {
            var result = _carRateService.delete(carRate);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("update")]
        public IActionResult Update(CarRate carRate)
        {
            var result = _carRateService.update(carRate);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
