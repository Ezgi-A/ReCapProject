using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.Entity_FrameWork
{
    public class EFCreditCardDal: EfEntityRepositoryBase<CreditCard, MyCarProjectDBContext>, ICreditCardDal
    {

    }
}
