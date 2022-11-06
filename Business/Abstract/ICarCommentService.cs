using Core.Utilites.DataResults;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICarCommentService : IServiceRepository<CarComment>
    {
        public IDataResult<List<CarComment>> GetByCarId(int carId);
    }
}
