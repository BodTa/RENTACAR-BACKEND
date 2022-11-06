
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;

namespace DataAccess.Concrete.EntityFramework;

public class EfUserCommentDal : EfEntityRepositoryBase<UserComment, SqlContext>, IUserCommentDal
{
}
