using Business.Abstract;
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
    public class CarRateManager : ICarRateService
    {
        private ICarRateDal _carRateDal;

        public CarRateManager(ICarRateDal carRateDal)
        {
            _carRateDal = carRateDal;
        }

        [ValidationAspect(typeof(CarRateValidator))]
        public IResult add(CarRate entity)
        {
            var result = _carRateDal.Get(r => r.CarId == entity.CarId && r.RaterId == entity.RaterId);
            if(result != null)
            {
                return new ErrorResult("Error");
            }
            _carRateDal.Add(entity);
            return new SuccessResult("Success");
        }

        public IResult delete(CarRate entity)
        {
            var result = _carRateDal.Get(r => r.CarId == entity.CarId && r.RaterId == entity.RaterId);
            _carRateDal.Delete(result);
            return new SuccessResult("Success");
        }

        public IDataResult<List<CarRate>> GetAll()
        {
            throw new NotImplementedException();
        }

        public IDataResult<CarRate> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IResult update(CarRate entity)
        {
            var result = _carRateDal.Get(r => r.CarId == entity.CarId && r.RaterId == entity.RaterId);
            _carRateDal.Update(result);
            return new SuccessResult("Success");
        }
    }
}
