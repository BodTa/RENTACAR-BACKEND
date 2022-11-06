using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilites.DataResults;
using Core.Utilites.Results;
using Core.Utilites.Security.Hashing;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IResult add(User entity)
        {
            var result = _userDal.GetAll().SingleOrDefault(u => u.Email == entity.Email);
            if (result == null)
            {
                _userDal.Add(entity);
                return new SuccessResult(Messages.Added);
            }
            else
            {
                return new ErrorResult("Böyle bir e-posta kullanılmaktadır.");
            }
        }

        public IResult delete(User entity)
        {
            _userDal.Delete(entity);
            return new SuccessResult(Messages.Deleted);
        }

        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll());
        }

        public IDataResult<User> GetByEmail(string email)
        {
            var user = _userDal.GetAll().SingleOrDefault(u => u.Email == email);
            return new SuccessDataResult<User>(user);
        }
        [SecuredOperation("user")]
        public IDataResult<UserDetailsDTO> GetDetailsById(int id)
        {
            return new SuccessDataResult<UserDetailsDTO>(_userDal.UserDetails().SingleOrDefault(u =>u.UserId==id));
        }

        public List<OperationClaim> GetClaims(User user)
        {
            return _userDal.GetClaims(user);
        }

        public IResult update(User entity)
        {
            _userDal.Update(entity);
            return new SuccessResult(Messages.Updated);
        }

        public IDataResult<List<UserDetailsDTO>> UserDetails()
        {
            return new SuccessDataResult<List<UserDetailsDTO>>(_userDal.UserDetails());
        }

        public IDataResult<User> GetById(int id)
        {
            var user = _userDal.Get(u => u.Id == id);
            return new SuccessDataResult<User>(user);
        }
        public IDataResult<UserDetailsDTO> GetDetailsByEmail(string email)
        {
            return new SuccessDataResult<UserDetailsDTO>(_userDal.GetDetailsByEmail(email));
        }

        public IResult UpdateByDto(UserForUpdateDTO userdto,int Id)
        {
            var user = _userDal.GetAll().SingleOrDefault(u => u.Id == Id);
            user.LastName = userdto.LastName;
            user.FirstName = userdto.FirstName;
            user.Email = userdto.Email;
            _userDal.Update(user);
            return new SuccessResult();
        }

        public IResult UpdatePassword(string password, string oldpassword, int Id)
        {
            var user = _userDal.GetAll().SingleOrDefault(u => u.Id == Id);
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(oldpassword, out passwordHash, out passwordSalt);
            if (passwordHash == user.PasswordHash && passwordSalt == user.PasswordSalt)
            {
                HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                _userDal.Update(user);
                return new SuccessResult();
            }
            else
            {
                return new ErrorResult();
            }
        }

        public IDataResult<UserDetailsDTO> GetDetailsByUserId(int id)
        {
            return new SuccessDataResult<UserDetailsDTO>(_userDal.UserDetails().SingleOrDefault(u => u.UserId == id));
        }
    }
}
