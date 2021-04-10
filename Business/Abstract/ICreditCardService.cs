using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICreditCardService
    {
        IDataResult<List<CreditCard>> GetAll();
        IResult Add(CreditCard creditCard);
        IResult Update(CreditCard creditCard);
        IResult Delete(CreditCard creditCard);
        IDataResult<CreditCard> Get(CreditCard creditCard);
        IResult GetList(List<CreditCard> list);
        IDataResult<CreditCard> FindByID(int Id);
        IDataResult <List<CreditCard>> GetByCustomerId(int customerId);
    }
}
