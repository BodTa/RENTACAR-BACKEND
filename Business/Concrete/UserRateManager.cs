using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
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
    internal class UserRateManager : IUserRateService
    {
        private IUserRateDal _userRateDal;

        public UserRateManager(IUserRateDal userRateDal)
        {
            _userRateDal = userRateDal;
        }
        [ValidationAspect(typeof(UserRateValidator))]
        [SecuredOperation("user")]
        public IResult add(UserRate entity)
        {
            var result = _userRateDal.Get(r => r.RaterId == entity.RaterId && r.UserId == entity.UserId);
            if(result != null)
            {
                entity.Id = result.Id;
                _userRateDal.Update(entity);
                return new SuccessResult("added");

            }
            _userRateDal.Add(entity);
            return new SuccessResult("added");
        }

        [SecuredOperation("user")]
        public IResult delete(UserRate entity)
        {
            var result = _userRateDal.Get(r => r.UserId == entity.UserId && r.RaterId == entity.RaterId);
            _userRateDal.Delete(result);
            return new SuccessResult("deleted");
        }

        public IDataResult<List<UserRate>> GetAll()
        {
            throw new NotImplementedException();
        }

        public IDataResult<UserRate> GetById(int id)
        {
            throw new NotImplementedException();
        }
        public IDataResult<UserRate> GetByRaterIdAndUserId(int id)
        {
            throw new NotImplementedException();
        }

        [SecuredOperation("user")]
        public IResult update(UserRate entity)
        {
            var result = _userRateDal.Get(r => r.UserId == entity.UserId && r.RaterId == entity.RaterId);
            _userRateDal.Update(result);
            return new SuccessResult("updated");
        }
    }
}
