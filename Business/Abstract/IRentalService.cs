﻿using Core.Utilites.DataResults;
using Core.Utilites.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
   public interface IRentalService : IServiceRepository<Rental>
    {
        public IDataResult<Rental> GetByCarId(int carId);
        public IResult IsRentable(int carId, DateTime rentDate);
    }
}
