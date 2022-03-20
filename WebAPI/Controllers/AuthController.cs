using Business.Abstract;
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
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public ActionResult Register(CustomerForRegisterDto userForRegisterDto)
        {
            var userexists = _authService.UserExists(userForRegisterDto.Email);
            if (!userexists.Success)
            {
                return BadRequest(userexists);
            }
            else
            {
                var registeredResult = _authService.Register(userForRegisterDto, userForRegisterDto.Password);
                if (registeredResult.Success)
                {
                    var result = _authService.CreateAccessToken(registeredResult.Data);
                    if (result.Success)
                    {
                        return Ok(result);
                    }
                    return BadRequest(result.Message);
                }
                return BadRequest(registeredResult);
            }
        }
        [HttpPost("login")]
        public ActionResult Login(UserForLoginDto userForLoginDto)
        {
            var isUserExist = _authService.UserExists(userForLoginDto.Email);
            if (isUserExist.Success)
            {
                return BadRequest(isUserExist.Message);
            }
            else
            {
                var userToLogin = _authService.Login(userForLoginDto);
                var result = _authService.CreateAccessToken(userToLogin.Data);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
        }
    }
}
