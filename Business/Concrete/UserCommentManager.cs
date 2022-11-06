using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Core.Utilites.DataResults;
using Core.Utilites.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserCommentManager : IUserCommentService
    {
        IUserCommentDal _userCommentDal;

        public UserCommentManager(IUserCommentDal userCommentDal)
        {
            _userCommentDal = userCommentDal;
        }
        [SecuredOperation("user")]
        public IResult add(UserComment entity)
        {
            _userCommentDal.Add(entity);
            return new SuccessResult("Successfull");
        }
        [SecuredOperation("user")]

        public IResult delete(UserComment entity)
        {
            var result = _userCommentDal.Get(c => c.Id == entity.Id);
            if (result == null)
                return new ErrorResult("Error");
            _userCommentDal.Delete(entity);
            return new SuccessResult("Successfull");
        }

        public IDataResult<List<UserComment>> GetAll()
        {
            throw new NotImplementedException();
        }

        public IDataResult<UserComment> GetById(int id)
        {
            throw new NotImplementedException();
        }
        [SecuredOperation("user")]

        public IResult update(UserComment entity)
        {
            _userCommentDal.Update(entity);
            return new SuccessResult("Successfull");
        }
        public IDataResult<List<UserComment>> GetByUserId(int userId)
        {
            return new SuccessDataResult<List<UserComment>>(_userCommentDal.GetAll(c=>c.UserId == userId));
        }
    }
}
