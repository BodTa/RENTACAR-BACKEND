using Core.Utilites.DataResults;
using Core.Utilites.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserPictureService
    {
        IResult add(IFormFile file, UserPicture picture);
        IResult update(IFormFile file, UserPicture entity);
        IResult delete(UserPicture entity);
        IDataResult<UserPicture> GetById(int id);
        IDataResult<UserPicture> GetByUserId(int userid);
        IDataResult<List<UserPicture>> GetAll();

    }
}
