
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilites.Business;
using Core.Utilites.DataResults;
using Core.Utilites.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        ICustomerDal _Customer;
        public CustomerManager(ICustomerDal customerDal)
        {
            _Customer = customerDal;
        }
        [CacheRemoveAspect("IColorService.Get")]
        [ValidationAspect(typeof(CustomerValidator))]
        public IResult add(Customer entity)
        {
            var result = BusinessRules.Run(CheckUserName(entity.FirstName, entity.LastName));
            if (result != null)
            {
                return new ErrorResult(result.Message);
            }
            _Customer.Add(entity);
            return new SuccessResult(Messages.Added);

        }
        [CacheRemoveAspect("IColorService.Get")]
        public IResult delete(Customer entity)
        {
            _Customer.Delete(entity);
            return new SuccessResult(Messages.Deleted);
        }
        [CacheAspect]
        public IDataResult<List<Customer>> GetAll()
        {
            return new SuccessDataResult<List<Customer>>(_Customer.GetAll(), Messages.Listed);
        }

        public IDataResult<Customer> GetById(int id)
        {
            return new SuccessDataResult<Customer>(_Customer.Get(p => p.Id == id), Messages.Listed);
        }
        [CacheRemoveAspect("IColorService.Get")]
        public IResult update(Customer entity)
        {
            _Customer.Update(entity);
            return new SuccessResult(Messages.Updated);
        }
        private IResult CheckUserName(string FirstName,string LastName)
        {
            string fullname = FirstName + LastName;
            bool result = fullname.Any(char.IsDigit);
            if (result)
            {
                return new ErrorResult("İsiminizde rakam olmamalıdır!");
            }
            return new SuccessResult();
        }
    }
}
