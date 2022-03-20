using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Helpers;
using Core.Utilites.Business;
using Core.Utilites.DataResults;
using Core.Utilites.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class ImageManager : IImageService
    {
        IImageDal _ımageDal;
        public ImageManager(IImageDal ımageDal)
        {
            _ımageDal = ımageDal;
            
        }
        [CacheRemoveAspect("IImageService.Get")]
        
        public IResult add(IFormFile file,Image entity)
        {
            var result = BusinessRules.Run(CheckIfImageCount(entity.CarId),CheckIfCarImageNull(entity.CarId));
            if (result!=null)
            {
                return result;
            }
            var ımage = FileHelper.Upload(file);
            if (!ımage.Success) 
            {
                return new ErrorResult(Messages.NotAdded);
            }
            entity.ImagePath = ımage.Message;
            _ımageDal.Add(entity);
            return new SuccessResult(Messages.Added);
        }
        [CacheRemoveAspect("IImageService.Get")]
        public IResult delete(Image entity)
        {
            var result = _ımageDal.Get(p => p.ImageId == entity.ImageId);
            _ımageDal.Delete(result);
            return new SuccessResult(Messages.Deleted);
        }
        [CacheAspect]
        public IDataResult<List<Image>> GetAll()
        {
            return new SuccessDataResult<List<Image>>(_ımageDal.GetAll(), Messages.Listed);
        }

        public IDataResult<Image> GetById(int id)
        {
            return new SuccessDataResult<Image>(_ımageDal.Get(p => p.ImageId == id),Messages.Listed);
        }

        public IDataResult<List<Image>> GeyByCarId(int carid)
        {
            return new SuccessDataResult<List<Image>>(_ımageDal.GetAll(c => c.CarId == carid), Messages.Listed);
        }
        [CacheRemoveAspect("IImageService.Get")]
        public IResult update(IFormFile file, Image entity)
        {

            var isImage = _ımageDal.Get(c => c.ImageId == entity.ImageId);
            if (isImage == null)
            {
                return new ErrorResult();
            }

            var updatedFile = FileHelper.Update(file, isImage.ImagePath);
            if (!updatedFile.Success)
            {
                return new ErrorResult(updatedFile.Message);
            }
            entity.ImagePath = updatedFile.Message;
            _ımageDal.Update(entity);
            return new SuccessResult(Messages.Updated);
        }

        private IResult CheckIfImageCount(int carid)
        {
            var result = _ımageDal.GetAll(p => p.CarId == carid).Count;
            if (result > 5)
            {
                return new ErrorResult(Messages.NotAdded);
            }
            return new SuccessResult();
        }
        private IDataResult<List<Image>> CheckIfCarImageNull(int id)
        {
            try
            {
                string path = @"\uploads\images \logo.jpg";
                var result = _ımageDal.GetAll(c => c.CarId == id).Any();
                if (!result)
                {
                    List<Image> carimage = new List<Image>();
                    carimage.Add(new Image { CarId = id, ImagePath = path, Date = DateTime.Now });
                    return new SuccessDataResult<List<Image>>(carimage);
                }
            }
            catch (Exception exception)
            {

                return new ErrorDataResult<List<Image>>(exception.Message);
            }

            return new SuccessDataResult<List<Image>>(_ımageDal.GetAll(p => p.CarId == id).ToList());
        }
    }
}
