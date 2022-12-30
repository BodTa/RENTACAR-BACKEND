using Business.Abstract;
using Entities.Concrete;
using Entities.DTOS;
using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        ICarService _carservice;
        public CarsController(ICarService carService)
        {
            _carservice = carService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {

            var result = _carservice.CarDetails();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _carservice.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getdetailsbyid")]
        public IActionResult GetDetailById(int id)
        {
            var result = _carservice.GetDetailById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("add")]
        public IActionResult Add(Car car)
        {
            var result = _carservice.add(car);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("addwithimages")]
        public IActionResult AddWithImages([FromForm] AddCarWithImagesDTO entity)
        {
            var result = _carservice.AddWithImages(entity);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("update")]
        public IActionResult Update(Car car)
        {
            var result = _carservice.update(car);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("delete")]
        public IActionResult Delete(Car car)
        {
            var result = _carservice.delete(car);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("getallbyprice")]
        public IActionResult GetAllByPrice(decimal min, decimal max)
        {
            var result = _carservice.GetAllByPrice(min, max);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }
        [HttpGet("cardetails")]
        public IActionResult CarDetails()
        {
            var result = _carservice.CarDetails();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getbybrand")]
        public IActionResult GetByBrand(int brandId)
        {
            var result = _carservice.GetByBrandId(brandId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getbycolor")]
        public IActionResult GetByColor(int colorId)
        {
            var result = _carservice.GetByColorId(colorId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getbybrandandcolor")]
        public IActionResult GetByBrandAndColor(int brandId, int colorId)
        {
            var result = _carservice.GetByBrandAndColor(brandId, colorId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getbysellerid")]

        public IActionResult getbysellerıd(int id)
        {
            var result = _carservice.GetAllBySellerId(id);
            int lastindex = result.Data.Count();
            var lastcar = result.Data[lastindex - 1];
            if (lastcar != null)
            {
                return Ok(lastcar);
            }
            return BadRequest();
        }
        [HttpGet("getcarsbysellerid")]

        public IActionResult getcarsbysellerıd(int id)
        {
            var result = _carservice.GetAllBySellerId(id);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpPost("deletewithcarid")]
        public IActionResult DeleteWithCarId([FromBody] int id)
        {
            var result = _carservice.DeleteWithId(id);
            return result.Success ? Ok(result) : BadRequest();
        }
    }
}
