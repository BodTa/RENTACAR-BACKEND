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
        IImageDal _imageDal;
        public ImageManager(IImageDal ımageDal)
        {
            _imageDal = ımageDal;
            
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
            _imageDal.Add(entity);
            return new SuccessResult(Messages.Added);
        }
        [CacheRemoveAspect("IImageService.Get")]
        public IResult delete(Image entity)
        {
            var result = _imageDal.Get(p => p.ImageId == entity.ImageId);
            _imageDal.Delete(result);
            return new SuccessResult(Messages.Deleted);
        }
        [CacheAspect]
        public IDataResult<List<Image>> GetAll()
        {
            return new SuccessDataResult<List<Image>>(_imageDal.GetAll(), Messages.Listed);
        }

        public IDataResult<Image> GetById(int id)
        {
            return new SuccessDataResult<Image>(_imageDal.Get(p => p.ImageId == id),Messages.Listed);
        }
        [CacheAspect]

        public IDataResult<List<Image>> GeyByCarId(int carid)
        {
            return new SuccessDataResult<List<Image>>(_imageDal.GetAll(c => c.CarId == carid), Messages.Listed);
        }
        [CacheRemoveAspect("IImageService.Get")]
        public IResult update(IFormFile file, Image entity)
        {

            var isImage = _imageDal.Get(c => c.ImageId == entity.ImageId);
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
            _imageDal.Update(entity);
            return new SuccessResult(Messages.Updated);
        }

        private IResult CheckIfImageCount(int carid)
        {
            var result = _imageDal.GetAll(p => p.CarId == carid).Count;
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
                var result = _imageDal.GetAll(c => c.CarId == id).Any();
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

            return new SuccessDataResult<List<Image>>(_imageDal.GetAll(p => p.CarId == id).ToList());
        }

        [CacheRemoveAspect("IImageService.Get")]
        public IResult AddMultiple(List<IFormFile> files,int carId)
        {
            foreach(IFormFile file in files)
            {
                var imagePath = FileHelper.Upload(file);
                
                if (imagePath.Success) { _imageDal.Add(new Image { CarId = carId, ImagePath = imagePath.Message }); }
                else { return new ErrorResult("Error Accured"); }
            }
            return new SuccessResult();
        }

        [CacheRemoveAspect("IImageService.Get")]
        public IResult DeleteWithCarId(int id)
        {
             var images = _imageDal.GetAll(c => c.CarId == id);
             foreach(Image image in images)
            {
                _imageDal.Delete(image);
            }
            return new SuccessResult();
        }
    }
}
