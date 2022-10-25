using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilites.DataResults;
using Core.Utilites.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
  public  class RentalManager : IRentalService
    {
        IRentalDal _Rent;
        public RentalManager(IRentalDal rentDal)
        {
            _Rent = rentDal;
        }
        [CacheRemoveAspect("IRentalService.Get")]
        [ValidationAspect(typeof(RentalValidator))]
        public IResult add(Rental entity)
        {
            var result = _Rent.GetAll().SingleOrDefault(p => p.CarId==entity.CarId && p.ReturnDate >= entity.RentDate/*&&p.RentDate >=entity.RentDate*/);
            if (result == null)
            {
                _Rent.Add(entity);
                return new SuccessResult(Messages.Added);
            }
            return new ErrorResult("Car is rented between given dates.");

        }
        [CacheRemoveAspect("IRentalService.Get")]
        public IResult delete(Rental entity)
        {
            _Rent.Delete(entity);
            return new SuccessResult(Messages.Deleted);
        }
        [CacheAspect]
        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_Rent.GetAll(), Messages.Listed);
        }

        public IDataResult<Rental> GetByCarId(int carId)
        {
            return new SuccessDataResult<Rental>(_Rent.Get(r => r.CarId == carId));
        }

        public IDataResult<Rental> GetById(int id)
        {
            return new SuccessDataResult<Rental>(_Rent.Get(p => p.RentalId == id), Messages.Listed);
        }

        public IResult IsRentable(int carId, DateTime rentDate)
        {
            var result = _Rent.GetAll(r => r.CarId == carId && r.ReturnDate >= rentDate);
            if (result.Count > 0)
            {
                return new ErrorResult("istediğiniz zaman aralığında araç kiralanmıştır.");
            }
            return new SuccessResult("Araç başarıyla kiralanabilir.");
         }

        [CacheRemoveAspect("IRentalService.Get")]
        public IResult update(Rental entity)
        {
            _Rent.Update(entity);
            return new SuccessResult(Messages.Updated);
        }

    }
}
