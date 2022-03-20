using Business.Abstract;
using Core.Entities.Concrete;
using Entities.Concrete;
using Entities.DTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("getall")]
        public IActionResult GetAll()
        {

            var result = _userService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getbyid")]
        public IActionResult GetDetailsById(int id)
        {
            var result = _userService.GetDetailsById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("add")]
        public IActionResult Add(User user)
        {
            var result = _userService.add(user);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("update")]
        public IActionResult Update(UserForUpdateDTO user,int Id)
        {
            var result =_userService.UpdateByDto(user,Id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("delete")]
        public IActionResult Delete(User user)
        {
            var result = _userService.delete(user);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getbyemail")]
        public IActionResult GetUserDetailsByEmail(string email)
        {
            var result = _userService.GetDetailsByEmail(email);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);  
        }
        [HttpPost("updatepassword")]
        public IActionResult UpdatePassword(string password, string oldpassword, int Id)
        {
            var result = _userService.UpdatePassword(password, oldpassword, Id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
