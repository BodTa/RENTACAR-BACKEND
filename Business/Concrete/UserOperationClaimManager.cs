using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilites.DataResults;
using Core.Utilites.Results;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserOperationClaimManager : IUserOperationClaimService
    {
        IUserOperationClaimDal IUserOperationClaimService;

        public UserOperationClaimManager(IUserOperationClaimDal ıUserOperationClaimService)
        {
            IUserOperationClaimService = ıUserOperationClaimService;
        }

        public IResult add(UserOperationClaim entity)
        {
            IUserOperationClaimService.Add(entity);
            return new SuccessResult();
        }

        public IResult delete(UserOperationClaim entity)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<UserOperationClaim>> GetAll()
        {
            throw new NotImplementedException();
        }

        public IDataResult<UserOperationClaim> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IResult update(UserOperationClaim entity)
        {
            throw new NotImplementedException();
        }
    }
}
