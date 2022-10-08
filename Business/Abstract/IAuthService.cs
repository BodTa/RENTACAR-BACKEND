using Core.Entities.Concrete;
using Core.Utilites.DataResults;
using Core.Utilites.Results;
using Core.Utilites.Security.Jwt;
using Entities.Concrete;
using Entities.DTOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<Customer> Register(CustomerForRegisterDto customerForRegisterDto, string password);
        IDataResult<User> Login(UserForLoginDto userForLoginDto);
        IResult UserExists(string email);
        IDataResult<AccessToken> CreateAccessToken(User user);

        IDataResult<RefreshToken> CreateRefreshToken(User user, string IpAdress);

        IDataResult<RefreshToken> AddRefreshToken(RefreshToken refreshToken);
        IDataResult<RefreshToken> GetRefreshToken(string Token);
    }
}
