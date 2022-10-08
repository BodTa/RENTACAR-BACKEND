    

using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilites.DataResults;
using Core.Utilites.Results;
using Core.Utilites.Security.Hashing;
using Core.Utilites.Security.Jwt;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOS;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userservice;
        private ITokenHelper tokenHelper;
        private ICustomerService customerService;
        private IRefreshTokenDal _refreshTokenDal;

        public AuthManager(IUserService userservice, ITokenHelper tokenHelper, ICustomerService customerService, IRefreshTokenDal refreshTokenDal)
        {
            _userservice = userservice;
            this.tokenHelper = tokenHelper;
            this.customerService = customerService;
            _refreshTokenDal = refreshTokenDal;
        }

        public IDataResult<RefreshToken> AddRefreshToken(RefreshToken refreshToken)
        {
            var createdToken = _refreshTokenDal.Add(refreshToken);
            return createdToken;
        }
        public IDataResult<RefreshToken> GetRefreshToken(string Token)
        {
            var refreshTokens = _refreshTokenDal.Get(r => r.Token == Token);
            if(refreshTokens == null)
            {
                return new ErrorDataResult<RefreshToken>("Invalid refresh token");
            }
            return new SuccessDataResult<RefreshToken>(refreshTokens);
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userservice.GetClaims(user);
            var accessToken = tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }

        public IDataResult<RefreshToken> CreateRefreshToken(User user, string IpAdress)
        {
            RefreshToken refreshToken = tokenHelper.CreateRefreshToken(user, IpAdress);
            return new SuccessDataResult<RefreshToken>(refreshToken);

        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userservice.GetByEmail(userForLoginDto.Email);
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }
            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.Data.PasswordHash, userToCheck.Data.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }
            return new SuccessDataResult<User>(userToCheck.Data, Messages.SuccessfulLogin);
        }
        public IDataResult<Customer> Register(CustomerForRegisterDto customerForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password,out passwordHash, out passwordSalt);
            var customer = new Customer
            {
                Email = customerForRegisterDto.Email,
                FirstName = customerForRegisterDto.FirstName,
                LastName = customerForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                CompanyName = customerForRegisterDto.CompanyName,
                Status = true
            };
            var result =customerService.add(customer);
            if  (result.Success){
                return new SuccessDataResult<Customer>(customer, Messages.UserRegistered);
            }
            return new ErrorDataResult<Customer>(customer, result.Message);
        }

        public IResult UserExists(string email)
        {
            if (_userservice.GetByEmail(email).Data != null)
            {
                return new SuccessResult(Messages.UserElreadyExists);

            }

            return new ErrorResult(Messages.UserNotFound);
        }
    }
}
