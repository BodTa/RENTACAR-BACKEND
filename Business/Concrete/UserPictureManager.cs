

using Business.Abstract;
using Core.Aspects.Autofac.Caching;
using Core.Helpers;
using Core.Utilites.DataResults;
using Core.Utilites.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Business.Concrete;

public class UserPictureManager : IUserPictureService
{
    IUserPictureDal _userPictureDal;

    public UserPictureManager(IUserPictureDal userPictureDal)
    {
        _userPictureDal = userPictureDal;
    }
    [CacheRemoveAspect("IUserPictureService.Get")]
    public IResult add(IFormFile file, UserPicture picture)
    {
        var image = FileHelper.Upload(file);
        if (!image.Success)
        {
            return new ErrorResult("Image can not added");
        }
        picture.ImagePath = image.Message;
        _userPictureDal.Add(picture);
        return new SuccessResult("Image successfuly added");
    }
    [CacheRemoveAspect("IUserPictureService.Get")]
    public IResult update(IFormFile file, UserPicture entity)
    {
        var isImage = _userPictureDal.Get(p => p.Id == entity.Id);
        if(isImage == null)
        {
            return new ErrorResult("Image can not updated.");
        }
        var updatedFile = FileHelper.Update(file,isImage.ImagePath);
        if (!updatedFile.Success)
        {
            return new ErrorResult("Image can not updated.");
        }
        entity.ImagePath = updatedFile.Message;
        _userPictureDal.Update(entity);
        return new SuccessResult("Image successfuly Updated.");
    }
    [CacheRemoveAspect("IUserPictureService.Get")]
    public IResult delete(UserPicture entity)
    {
        var result = _userPictureDal.Get(p => p.Id == entity.Id);
        _userPictureDal.Delete(result);
        return new SuccessResult("Image successfuly deleted");
    }

    public IDataResult<UserPicture> GetById(int id)
    {
        return new SuccessDataResult<UserPicture>(_userPictureDal.Get(p => p.Id == id));
    }

    public IDataResult<UserPicture> GetByUserId(int userid) 
    {
        return new SuccessDataResult<UserPicture>(_userPictureDal.Get(p => p.UserId == userid));  
    }

    public IDataResult<List<UserPicture>> GetAll()
    {
        throw new System.NotImplementedException();
    }
}
