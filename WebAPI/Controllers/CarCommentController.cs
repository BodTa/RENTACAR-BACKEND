using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarCommentController : ControllerBase
{
    ICarCommentService _carCommentService;

    public CarCommentController(ICarCommentService carCommentService)
    {
        _carCommentService = carCommentService;
    }
    [HttpGet("getbycarid")]
    public IActionResult GetByUserId(int id)
    {
        var result = _carCommentService.GetByCarId(id);
        if(result.Success)
            return Ok(result.Data);
        return BadRequest(result);
    }

    [HttpPut("add")]
    public IActionResult Add(CarComment comment)
    {
        var result = _carCommentService.add(comment);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }
    [HttpDelete("delete")]
    public IActionResult Delete(CarComment carComment)
    {
        var result = _carCommentService.delete(carComment);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }
    [HttpPost("update")]
    public IActionResult Update(CarComment comment)
    {
        var result = _carCommentService.update(comment);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }
}
