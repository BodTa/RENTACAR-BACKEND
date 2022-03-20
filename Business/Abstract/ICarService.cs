using Core.Utilites.DataResults;
using Entities.Concrete;
using Entities.DTOS;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Business.Abstract
{
    public interface ICarService:IServiceRepository<Car>
    {

        IDataResult<List<Car>> GetAllByPrice(decimal min, decimal max);

        IDataResult<List<CarDetailsDTO>> CarDetails(Expression<Func<CarDetailsDTO, bool>> filter = null);
        IDataResult<List<CarDetailsDTO>> GetByBrandId(int brandId);
        IDataResult<List<CarDetailsDTO>> GetByColorId(int colorId);
        IDataResult<List<CarDetailsDTO>> GetByBrandAndColor(int brandId, int colorId);
        IDataResult<List<CarDetailsDTO>> GetAllBySellerId(int sellerId);
        IDataResult<CarDetailsDTO> GetDetailById(int id);
    }
}
