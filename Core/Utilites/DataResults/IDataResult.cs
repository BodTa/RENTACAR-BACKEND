using Core.Utilites.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilites.DataResults
{
    public interface IDataResult<T> : IResult
    {
        public T Data { get; }
    }
}
