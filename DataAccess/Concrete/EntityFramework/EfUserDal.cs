﻿using Core.DataAccess.EntityFramework;
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
            using(var context= new SqlContext())
            {
                var result = from user in context.Users
                             select new UserDetailsDTO { UserId = user.Id, Email = user.Email, FirstName = user.FirstName, LastName = user.LastName };
                return result.ToList();
            }
        }
    }
}