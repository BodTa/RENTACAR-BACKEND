using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Entities.DTOS;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, SqlContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new SqlContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                                 on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
                return result.ToList();
            }
        }
        public UserDetailsDTO GetDetailsByEmail(string email)
        {
            using (var context = new SqlContext())
            {
                var result = from user in context.Users
                             where user.Email == email
                             select new UserDetailsDTO
                             {
                                 TelNumber = user.TelNumber,
                                 Email = user.Email,
                                 FirstName = user.FirstName,
                                 LastName = user.LastName,
                                 UserId = user.Id,
                                 Rates = context.UserRates.Where(r => r.UserId == user.Id).ToList(),
                                 UserPicture = context.UserPictures.FirstOrDefault(p => p.UserId == user.Id),
                             };
                return result.FirstOrDefault();
            }
        }
        public User GetByEmail(string email)
        {
            using(var context = new SqlContext())
            {
                var result = from user in context.Users
                             where user.Email == email
                             select user;
                return result.FirstOrDefault();
            }
        }

        public List<UserDetailsDTO> UserDetails(Expression<Func<User, bool>> filter = null)
        {
            using (SqlContext context = new SqlContext())
            {
                var userDetail = from user in filter is null ? context.Users : context.Users.Where(filter)
                                 select new UserDetailsDTO
                                 {
                                     UserId = user.Id,
                                     FirstName = user.FirstName,
                                     TelNumber = user.TelNumber,
                                     LastName = user.LastName,
                                     Email = user.Email,
                                     Rates = context.UserRates.Where(r => r.UserId == user.Id).ToList(),

                                     UserPicture = context.UserPictures.FirstOrDefault(p => p.UserId == user.Id),
                                 };
                return userDetail.ToList();
            }
        }
    }
}
