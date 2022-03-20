using Core.DataAccess;
using Core.Entities.Concrete;
using Entities.DTOS;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        List<OperationClaim> GetClaims(User user);

        User GetByEmail(string email);
        List<UserDetailsDTO> UserDetails(Expression<Func<User, bool>> filter = null);
    }
}
