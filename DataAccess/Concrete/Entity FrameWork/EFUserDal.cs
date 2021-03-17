using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataAccess.Concrete.Entity_FrameWork
{
    public class EFUserDal : EfEntityRepositoryBase<User, MyCarProjectDBContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new MyCarProjectDBContext())
            {
                var result = from op in context.OperationClaims
                             join us in context.UserOperationClaims
                             on op.Id equals us.OperationClaimId
                             where us.UserId==user.UserId
                             select new OperationClaim { Id = op.Id, Name = op.Name };
                return result.ToList();

                             

            }
        }
    }
}
