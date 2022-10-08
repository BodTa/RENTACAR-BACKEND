using Core.DataAccess;
using Core.Entities;
using Core.Utilites.DataResults;
using Core.Utilites.Security.Jwt;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IRefreshTokenDal
    {
        public IDataResult<RefreshToken> Add(RefreshToken refreshToken);
        RefreshToken Get(Expression<Func<RefreshToken, bool>> filter);

    }
}
