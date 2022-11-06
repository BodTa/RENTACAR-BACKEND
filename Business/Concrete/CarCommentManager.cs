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
    public class CarCommentManager : ICarCommentService
    {
        ICarCommentDal _carCommentDal;

        public CarCommentManager(ICarCommentDal carCommentDal)
        {
            _carCommentDal = carCommentDal;
        }
        [SecuredOperation("user")]

        public IResult add(CarComment entity)
        {
            _carCommentDal.Add(entity);
            return new SuccessResult("Success");
        }
        [SecuredOperation("user")]

        public IResult delete(CarComment entity)
        {
            var result = _carCommentDal.Get(c => c.Id == entity.Id);
            if(result != null)
            {
                _carCommentDal.Delete(entity);
                return new SuccessResult("Success");
            }
            return new ErrorResult("Error");
        }

        public IDataResult<List<CarComment>> GetAll()
        {
            throw new NotImplementedException();
        }

        public IDataResult<CarComment> GetById(int id)
        {
            throw new NotImplementedException();
        }
        [SecuredOperation("user")]

        public IResult update(CarComment entity)
        {
            var result = _carCommentDal.Get(c => c.Id == entity.Id);
            if (result != null)
            {
                _carCommentDal.Update(entity);
                return new SuccessResult("Success");
            }
            return new ErrorResult("Error");
        }
        public IDataResult<List<CarComment>> GetByCarId(int carId)
        {
            return new SuccessDataResult<List<CarComment>>(_carCommentDal.GetAll(c => c.CarId == carId));
        }
    }
}
