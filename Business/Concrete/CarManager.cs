using Business.Abstract;

using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilites.DataResults;
using Core.Utilites.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;
        public CarManager(ICarDal CarDal)
        {
            _carDal = CarDal;
        }

        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]
        //[SecuredOperation("admin,car.add")]
        public IResult add(Car entity)
        {
            _carDal.Add(entity);
            return new SuccessResult(Messages.Added);
            
        }

        [CacheAspect]
        public IDataResult<List<CarDetailsDTO>> CarDetails(Expression<Func<CarDetailsDTO, bool>> filter = null)
        {
            return new SuccessDataResult<List<CarDetailsDTO>>(_carDal.CarDetails(), Messages.Listed);
        }
        [CacheRemoveAspect("ICarService.Get")]
        public IResult delete(Car entity)
        {
            _carDal.Delete(entity);
            return new SuccessResult(Messages.Deleted);
        }
        [CacheAspect]
        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.Listed);
        }
        [CacheAspect]
        public IDataResult<List<Car>> GetAllByPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(p => p.DailyPrice >= min && p.DailyPrice <= max), Messages.Listed);
        }

        public IDataResult<List<CarDetailsDTO>> GetAllBySellerId(int sellerId)
        {
            return new SuccessDataResult<List<CarDetailsDTO>>(_carDal.CarDetails(c => c.SellerId == sellerId));
        }

        public IDataResult<List<CarDetailsDTO>> GetByBrandAndColor(int brandId, int colorId)
        {
            return new SuccessDataResult<List<CarDetailsDTO>>(_carDal.CarDetails(c=>c.ColorId==colorId&&c.BrandId==brandId),Messages.Listed);
        }

        public IDataResult<List<CarDetailsDTO>> GetByBrandId(int brandId)
        {
            return new SuccessDataResult<List<CarDetailsDTO>>(_carDal.CarDetails(p=>p.BrandId==brandId),Messages.Listed);
        }

        public IDataResult<List<CarDetailsDTO>> GetByColorId(int colorId)
        {
            return new SuccessDataResult<List<CarDetailsDTO>>(_carDal.CarDetails(p => p.ColorId == colorId), Messages.Listed);
        }

        public IDataResult<Car> GetById(int id)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c=>c.CarId==id));
        }

        public IDataResult<CarDetailsDTO> GetDetailById(int id)
        {
            return new SuccessDataResult<CarDetailsDTO>(_carDal.CarDetails().SingleOrDefault(c=>c.CarId==id), Messages.Listed);
        }
        [CacheRemoveAspect("ICarService.Get")]
        public IResult update(Car entity)
        {
            _carDal.Update(entity);
            return new SuccessResult(Messages.Updated);
        }
    }
}
