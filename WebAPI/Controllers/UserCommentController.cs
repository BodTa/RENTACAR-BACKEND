using Business.Abstract;
using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserCommentController : ControllerBase
{
    IUserCommentService _userCommentService;

    public UserCommentController(IUserCommentService userCommentService)
    {
        _userCommentService = userCommentService;
    }

    [HttpGet("getbyuserid")]
    public IActionResult GetByUserId(int userId)
    {
        var result   = _userCommentService.GetByUserId(userId);
        if (result.Success)
            return Ok(result.Data);
        return BadRequest(result);
    }
    [HttpPut("add")]
    public IActionResult Add(UserComment userComment)
    {
        var result = _userCommentService.add(userComment);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }
    [HttpPost("update")]
    public IActionResult Update(UserComment userComment)
    {
        var result = _userCommentService.update(userComment);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }
    [HttpDelete("delete")]
    public IActionResult Delete(UserComment userComment)
    {
        var result = _userCommentService.delete(userComment);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }
}
