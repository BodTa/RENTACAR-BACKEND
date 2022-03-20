using Core.Utilites.DataResults;
using Core.Utilites.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
   public interface IServiceRepository<T>
    {
        IResult add(T entity);
        IResult update(T entity);
        IResult delete(T entity);
        IDataResult<T> GetById(int id);
        IDataResult<List<T>> GetAll();
    }
}
