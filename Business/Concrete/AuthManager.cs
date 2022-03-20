    

using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilites.DataResults;
using Core.Utilites.Results;
using Core.Utilites.Security.Hashing;
using Core.Utilites.Security.Jwt;
using Entities.Concrete;
using Entities.DTOS;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userservice;
        private ITokenHelper tokenHelper;
        private ICustomerService customerService;
        public AuthManager(IUserService userService, ITokenHelper tokenHelper, ICustomerService customerService)
        {
            _userservice = userService;
            this.customerService = customerService;
            this.tokenHelper = tokenHelper;
                
        }
        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userservice.GetClaims(user);
            var accessToken = tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
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
                return new ErrorResult(Messages.UserElreadyExists);

            }

            return new SuccessResult(Messages.UserNotFound);
        }
    }
}
