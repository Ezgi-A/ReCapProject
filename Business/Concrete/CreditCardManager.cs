using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class CreditCardManager : ICreditCardService
    {
        ICreditCardDal _creditCardDal;
        public CreditCardManager(ICreditCardDal creditCardDal)
        {
            _creditCardDal = creditCardDal;
        }
        public IResult Add(CreditCard creditCard)
        {
            _creditCardDal.Add(creditCard);
            return new SuccessResult("Credit Card:" + Messages.CardAdded);
        }

        public IResult Delete(CreditCard creditCard)
        {
            _creditCardDal.Delete(creditCard);
            return new SuccessResult("Credit Card:" + Messages.CardDeleted);
        }

        public IDataResult<CreditCard> FindByID(int Id)
        {
            CreditCard p = new CreditCard();
            if (_creditCardDal.GetAll().Any(x => x.CreditCardId == Id))
            {
                p = _creditCardDal.GetAll().FirstOrDefault(x => x.CreditCardId == Id);
            }
            else Console.WriteLine(Messages.NotExist + "credit card");
            return new SuccessDataResult<CreditCard>(p);
        }

        public IDataResult<CreditCard> Get(CreditCard creditCard)
        {
            return new SuccessDataResult<CreditCard>(_creditCardDal.Get(ca => ca.CreditCardId == creditCard.CreditCardId));
        }

        public IDataResult<List<CreditCard>> GetAll()
        {
            return new SuccessDataResult<List<CreditCard>>(_creditCardDal.GetAll());
        }

        public IDataResult<List<CreditCard>> GetByCustomerId(int customerId)
        {
            return new SuccessDataResult<List<CreditCard>>(_creditCardDal.GetAll(c => c.CustomerID == customerId));
        }

        public IResult GetList(List<CreditCard> list)
        {
            throw new NotImplementedException();
        }

        public IResult Update(CreditCard creditCard)
        {
            _creditCardDal.Update(creditCard);
            return new SuccessResult("Credit Card:" + Messages.CardUpdated);
        }
    }
}
