using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Concrete.Entity_FrameWork
{
    public class EFCustomerDal:EfEntityRepositoryBase<Customer, MyCarProjectDBContext>,ICustomerDal
    {
        public List<CustomerDetailDto> GetCustomersDetail(Expression<Func<Customer, bool>> filter = null)
        {
            using (MyCarProjectDBContext context = new MyCarProjectDBContext())
            {
                var result = from cu in filter == null ? context.Customers : context.Customers.Where(filter)
                             join u in context.Users
                             on cu.UserId equals u.UserId
                             

                             select new CustomerDetailDto
                             {
                                 CustomerId = cu.CustomerId,
                                 FirstName = u.FirstName,
                                 LastName = u.LastName,
                                 Email = u.Email,
                                 CompanyName = cu.CompanyName,
                                 Findex=cu.Findex
                             };
                return result.ToList();
            }

        }
    }

}

