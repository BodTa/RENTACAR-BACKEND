using Core.Utilites.DataResults;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IFavoriteService : IServiceRepository<Favorite>
    {
        public IDataResult<List<Favorite>>  GetByUserId(int userId);
        public IDataResult<Favorite> GetByCarId(int carId);
    }
}
