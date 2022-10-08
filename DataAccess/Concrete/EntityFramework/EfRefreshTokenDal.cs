using Core.DataAccess.EntityFramework;
using Core.Entities;
using Core.Utilites.DataResults;
using Core.Utilites.Security.Jwt;
using DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRefreshTokenDal:IRefreshTokenDal
    {
        public IDataResult<RefreshToken> Add(RefreshToken entity)
        {
            using (SqlContext context = new SqlContext())
            {
                context.Entry(entity).State = EntityState.Added;
                 context.SaveChanges();
                return new SuccessDataResult<RefreshToken>(entity);
            }
        }

        public RefreshToken Get(Expression<Func<RefreshToken, bool>> filter)
        {
            using (SqlContext context = new SqlContext())
            {
                return context.Set<RefreshToken>().SingleOrDefault(filter);
            }
        }
    }
}
