using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilites.DataResults;
using Core.Utilites.Security.Jwt;
using Entities.DTOS;
using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;
        private IUserService _userService;

        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("register")]
        public ActionResult Register([FromBody] CustomerForRegisterDto userForRegisterDto)
        {
            var userexists = _authService.UserExists(userForRegisterDto.Email);
            if (userexists.Success)
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
            if (!isUserExist.Success)
            {
                return BadRequest(isUserExist.Message);
            }
            else
            {
                 var userToLogin = _authService.Login(userForLoginDto);
                if(userToLogin.Success)
                {
                    var accessToken = _authService.CreateAccessToken(userToLogin.Data);
                    var refreshToken = _authService.CreateRefreshToken(userToLogin.Data, IpAdress: GetIpAddress());
                    var addedRefreshToken = _authService.AddRefreshToken(refreshToken.Data);
                    AddRefreshTokenToCookie(refreshToken.Data);
                    if (accessToken.Success || refreshToken.Success || addedRefreshToken.Success)
                    {
                        return Ok(new LoginDto { RefreshToken = refreshToken.Data, AccessToken = accessToken.Data });
                    }
                }
                

                return BadRequest("Email or Password wrong.");
            }
        }
        [HttpGet("refresh")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var result = _authService.GetRefreshToken(refreshToken);
            if(result.Success)
            {
                IDataResult<User> user = new SuccessDataResult<User>();
                user = _userService.GetById(result.Data.UserId);
                var token = _authService.CreateAccessToken(user.Data);
                return token.Data.Token;
            }
            return ("Invalid Refresh Token");
        } 
        protected string? GetIpAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For")) return Request.Headers["X-Forwarded-For"];
            return HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();
        }
        protected void AddRefreshTokenToCookie(RefreshToken refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshToken.Expires,
            };
            Response.Cookies.Append("refreshToken",refreshToken.Token, cookieOptions);
            
        }
    }
}
