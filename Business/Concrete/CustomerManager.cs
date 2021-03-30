using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        ICustomerDal _ıcustomerDal;
        public CustomerManager(ICustomerDal ıcustomerdal)
        {
            _ıcustomerDal = ıcustomerdal;
        }
        public IResult Add(Customer customer)
        {
            _ıcustomerDal.Add(customer);
            return new SuccessResult(Messages.CustomerAdding);
        }

        public IResult Delete(Customer customer)
        {
            _ıcustomerDal.Delete(customer);
            return new SuccessResult(Messages.CustomerDeleting);
        }

        public IDataResult<List<Customer>> GetAll()
        {
            if (DateTime.Now.Hour==22)
            {
                return new ErrorDataResult<List<Customer>>(_ıcustomerDal.GetAll(), Messages.Maintenance);
            }
            return new SuccessDataResult<List<Customer>>(_ıcustomerDal.GetAll(), Messages.RentalListing);
        }

        public IDataResult<Customer> GetById(int Customerid)
        {
            return new SuccessDataResult<Customer>(_ıcustomerDal.Get(c => c.CustomerId == Customerid));
        }

        public IDataResult<List<CustomerDetailDto>> GetCustomersDetail()
        {
            return new SuccessDataResult<List<CustomerDetailDto>>(_ıcustomerDal.GetCustomersDetail());
        }

        public IResult Update(Customer customer)
        {
            _ıcustomerDal.Update(customer);
            return new SuccessResult(Messages.CustomerUpdating);
        }
    }
}
