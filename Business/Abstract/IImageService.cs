using Core.Utilites.DataResults;
using Core.Utilites.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
 public   interface IImageService

    {
        IResult add(IFormFile file,Image image);
        IResult update(IFormFile file,Image entity);
        IResult delete(Image entity);
        IDataResult<Image> GetById(int id);
        IDataResult<List<Image>> GeyByCarId(int carid);
        IDataResult<List<Image>> GetAll();
    }
}
