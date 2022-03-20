using Business.Abstract;
using Core.Utilites.DataResults;
using Core.Utilites.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CreditCartManager : ICreditCartService
    {
        ICreditCartDal _creditCartDal;
        public CreditCartManager(ICreditCartDal credit)
        {
            _creditCartDal = credit;
        }

        public IResult add(CreditCart entity)
        {
            throw new NotImplementedException();
        }

        public IResult delete(CreditCart entity)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<CreditCart>> GetAll()
        {
            throw new NotImplementedException();
        }

        public IDataResult<CreditCart> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IResult IsEnough(CreditCart creditcart, DateTime RentDate, DateTime ReturnDate)
        {
            throw new NotImplementedException();
        }

        public IResult Pay(CreditCart creditCart, DateTime RentDate, DateTime ReturnDate)
        {
            throw new NotImplementedException();
        }

        public IResult update(CreditCart entity)
        {
            throw new NotImplementedException();
        }
    }
}
