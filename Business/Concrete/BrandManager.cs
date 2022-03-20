using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilites.DataResults;
using Core.Utilites.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        IBrandDal _Brand;
        public BrandManager(IBrandDal brandDal)
        {
            _Brand = brandDal;
        }

        [ValidationAspect(typeof(BrandValidator))]
        [CacheRemoveAspect("IBrandService.Get")] // IProduct.Get patternimiz olucak.
        public IResult add(Brand entity)
        {
            _Brand.Add(entity);
            return new SuccessResult(Messages.Added);
        }

        [CacheRemoveAspect("IBrandService.Get")]
        public IResult delete(Brand entity)
        {
            _Brand.Delete(entity);
            return new SuccessResult(Messages.Deleted);
        }
        [CacheAspect]
        public IDataResult<List<Brand>> GetAll()
        {
            return new SuccessDataResult<List<Brand>>(_Brand.GetAll(), Messages.Listed);
        }


        [CacheAspect]
        public IDataResult<Brand> GetById(int id)
        {
            return new SuccessDataResult<Brand>(_Brand.Get(p => p.BrandId == id),Messages.Listed);
        }
        [CacheRemoveAspect("IBrandService.Get")]
        public IResult update(Brand entity)
        {
            _Brand.Update(entity);
            return new SuccessResult(Messages.Updated);
        }
    }
}
