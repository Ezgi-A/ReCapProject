using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataAccess.Concrete.Entity_FrameWork
{
    public class EFCustomerDal:EfEntityRepositoryBase<Customer, MyCarProjectDBContext>,ICustomerDal
    {
        public List<CustomerDetailDto> GetCustomersDetail()
        {
            using (MyCarProjectDBContext context = new MyCarProjectDBContext())
            {
                var result = from cu in  context.Customers
                             join u in context.Users
                             on cu.UserId equals u.UserId
                             

                             select new CustomerDetailDto
                             {
                                 CustomerId = cu.CustomerId,
                                 FirstName = u.FirstName,
                                 LastName = u.LastName,
                                 Email = u.Email,
                                 CompanyName = cu.CompanyName
                             };
                return result.ToList();
            }

        }
    }

}

