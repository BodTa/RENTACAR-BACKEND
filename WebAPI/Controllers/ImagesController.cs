using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        IImageService _ımagesService;
        public ImagesController(IImageService ımageService)
        {
            _ımagesService = ımageService;
        }
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _ımagesService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _ımagesService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getbycarid")]
        public IActionResult GetByCarId(int carid)
        {
            var result = _ımagesService.GeyByCarId(carid);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("add")]
        public IActionResult Add(IFormFile file, [FromForm(Name = ("Id"))] Image image)
        {
            var result = _ımagesService.add(file, image);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("addmultiple")]
        public IActionResult AddMultiple(List<IFormFile> files, [FromForm(Name = ("Id"))] int carId)
        {
            var result = _ımagesService.AddMultiple(files, carId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }

        [HttpPost("delete")]
        public IActionResult Delete([FromForm(Name = ("Id"))] int Id)
        {
            var result = _ımagesService.GetById(Id);
            var result2 = _ımagesService.delete(result.Data);
            if (result2.Success)
            {
                return Ok(result2);
            }
            return BadRequest(result2);
        }
        [HttpPost("update")]
        public IActionResult Update([FromForm(Name = ("Image"))] IFormFile file, [FromForm(Name = ("Id"))] int Id)
        {
            var entity = _ımagesService.GetById(Id);
            var result = _ımagesService.update(file, entity.Data);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
