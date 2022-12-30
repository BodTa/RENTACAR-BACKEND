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
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        private ICarDal _carDal;
        private IImageService _imageService;

        public CarManager(ICarDal carDal, IImageService imageService)
        {
            _carDal = carDal;
            _imageService = imageService;
        }

        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]
        //[SecuredOperation("admin,car.add")]
        public IResult add(Car entity)
        {
            _carDal.Add(entity);
            return new SuccessResult(Messages.Added);

        }

        [CacheRemoveAspect("ICarService.Get")]
        [SecuredOperation("user")]
        public IResult AddWithImages(AddCarWithImagesDTO entity)
        {
            Car car = new Car { BrandId = entity.BrandId, CarType = Convert.ToString(entity.CarType), CarModel = Convert.ToString(entity.CarModel), ColorId = entity.ColorId, DailyPrice = entity.DailyPrice, Description = entity.Description, DoorCount = entity.DoorCount, EngineCapacity = entity.EngineCapacity, FuelType = entity.FuelType, GearType = entity.GearType, HorsePower = entity.HorsePower, Kilometer = entity.Kilometer, ModelYear = entity.ModelYear, SellerId = entity.SellerId };
            _carDal.Add(car);
            Thread.Sleep(1000);
            var usersCar = _carDal.GetAll(c => c.SellerId == car.SellerId);
            var addedCar = usersCar.Where(c => c.Kilometer == entity.Kilometer && c.Description == entity.Description).FirstOrDefault();
            var result = _imageService.AddMultiple(entity.files, addedCar.CarId);
            return result.Success ? new SuccessResult() : new ErrorResult();

     
        }

        [CacheAspect]
        //[SecuredOperation("user , admin")]
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

        [CacheRemoveAspect("ICarService.Get")]
        [SecuredOperation("user")]

        public IResult DeleteWithId(int id)
        {
            var car = _carDal.Get(c => c.CarId == id);
            _carDal.Delete(car);
            _imageService.DeleteWithCarId(id);
            return new SuccessResult(); 
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
        [CacheAspect]

        public IDataResult<List<CarDetailsDTO>> GetAllBySellerId(int sellerId)
        {
            return new SuccessDataResult<List<CarDetailsDTO>>(_carDal.CarDetails(c => c.SellerId == sellerId));
        }
        [CacheAspect]

        public IDataResult<List<CarDetailsDTO>> GetByBrandAndColor(int brandId, int colorId)
        {
            return new SuccessDataResult<List<CarDetailsDTO>>(_carDal.CarDetails(c=>c.ColorId==colorId&&c.BrandId==brandId),Messages.Listed);
        }
        [CacheAspect]

        public IDataResult<List<CarDetailsDTO>> GetByBrandId(int brandId)
        {
            return new SuccessDataResult<List<CarDetailsDTO>>(_carDal.CarDetails(p=>p.BrandId==brandId),Messages.Listed);
        }
        [CacheAspect]

        public IDataResult<List<CarDetailsDTO>> GetByColorId(int colorId)
        {
            return new SuccessDataResult<List<CarDetailsDTO>>(_carDal.CarDetails(p => p.ColorId == colorId), Messages.Listed);
        }

        public IDataResult<Car> GetById(int id)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c=>c.CarId==id));
        }
        [CacheAspect]

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
