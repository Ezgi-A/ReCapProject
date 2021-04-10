﻿using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICustomerService
    {
        IDataResult<List<CustomerDetailDto>> GetCustomersDetail();
        IDataResult<List<Customer>> GetAll();
        IDataResult<List<CustomerDetailDto>> GetById(int Customerid);

        IResult Add(Customer customer);

        IResult Delete(Customer customer);
        IResult Update(Customer customer);
    }
}
