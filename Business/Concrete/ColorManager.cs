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
   public class ColorManager : IColorService
    {
        IColorDal _Color;
        public ColorManager(IColorDal ColorDal)
        {
            _Color = ColorDal;
        }
        [CacheRemoveAspect("IColorService.Get")]
        [ValidationAspect(typeof(ColorValidator))]
        public IResult add(Color entity)
        {
            _Color.Add(entity);
            return new SuccessResult(Messages.Added);
        }
        [CacheRemoveAspect("IColorService.Get")]
        public IResult delete(Color entity)
        {
            _Color.Delete(entity);
            return new SuccessResult(Messages.Deleted);
        }
        [CacheAspect]
        public IDataResult<List<Color>> GetAll()
        {
            return new SuccessDataResult<List<Color>>(_Color.GetAll(), Messages.Listed);
        }

        public IDataResult<Color> GetById(int id)
        {
            return new SuccessDataResult<Color>(_Color.Get(p => p.ColorId == id), Messages.Listed);
        }
        [CacheRemoveAspect("IColorService.Get")]
        public IResult update(Color entity)
        {
            _Color.Update(entity);
            return new SuccessResult(Messages.Updated);
        }
    }
}
