using Business.Abstract;
using Core.Entities;
using Core.Entities.Concrete;
using Core.Utilites.Business;
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
    public class FavoriteManager : IFavoriteService
    {
        IFavoriteDal _favoriteDal;

        public FavoriteManager(IFavoriteDal favoriteDal)
        {
            _favoriteDal = favoriteDal;
        }

        public IResult add(Favorite entity)
        {
            var result = BusinessRules.Run(CheckIfAlreadyExist(entity.UserId, entity.CarId));
            if(!result.Success)
            {
                _favoriteDal.Add(entity);
                return new SuccessResult("Car added to the favorite car(s).");
            }
            return new ErrorResult("Car aldready in favorite car(s).");
        }

        public IResult delete(Favorite entity)
        {
            _favoriteDal.Delete(entity);
             return new SuccessResult("Car successfuly deleted from favorite car(s).");
        }

        public IDataResult<List<Favorite>> GetAll()
        {
            return new SuccessDataResult<List<Favorite>>(_favoriteDal.GetAll());

        }

        public IDataResult<Favorite> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<Favorite>> GetByUserId(int userId)
        {
            return new SuccessDataResult<List<Favorite>>(_favoriteDal.GetAll(f => f.UserId == userId));

        }
        public IDataResult<Favorite> GetByCarId(int carId)
        {
            var result = _favoriteDal.Get(f => f.CarId == carId);
            return new SuccessDataResult<Favorite>(result);
        }
        public IResult update(Favorite entity)
        {
            var result = BusinessRules.Run(CheckIfAlreadyExist(entity.UserId, entity.CarId));
            if (!result.Success)
            {
                _favoriteDal.Update(entity);
                return new SuccessResult("Car successfuly updated.");
            }
            return new ErrorResult("Car cannot updated.");
        }

        private IResult CheckIfAlreadyExist(int userId,int carId)
        {
            var favCar = _favoriteDal.Get(f => f.CarId == carId && f.UserId == userId);
            if(favCar==null)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }
    }
}
