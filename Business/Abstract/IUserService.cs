using Core.Entities.Concrete;
using Core.Utilites.DataResults;
using Core.Utilites.Results;
using Entities.DTOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
   public interface IUserService: IServiceRepository<User>
    {
        List<OperationClaim> GetClaims(User user);
        IDataResult<User> GetByEmail(string email);
        IDataResult<UserDetailsDTO> GetDetailsByEmail(string email);
        IDataResult<UserDetailsDTO> GetDetailsById(int id);
        IDataResult <List<UserDetailsDTO>> UserDetails();

        IDataResult<UserDetailsDTO> GetDetailsByUserId(int id);
        IResult UpdateByDto(UserForUpdateDTO user,int Id);
        IResult UpdatePassword(string password, string oldpassword,int Id);
    }
}
