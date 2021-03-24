using Business.Abstract;
using Business.Constants;
using Core.Utilities.BusinessRules;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class PaymentManager : IPaymentService
    {
        IPaymentDal _paymentDal;
        ICreditCardService _creditCardService;
        public PaymentManager(IPaymentDal paymentDal, ICreditCardService creditCardService)
        {
            _paymentDal = paymentDal;
            _creditCardService = creditCardService;

        }
        public IResult Add(Payment payment)
        {
            IResult result = BusinessRules.Run(CheckIsCardValid(payment.CreditCardNumber, payment.ExpirationDate,
                payment.SecurityCode));
            if (result!=null)
            {
                return result;
            }
            _paymentDal.Add(payment);
            return new SuccessResult();
            
        }

        public IResult Delete(Payment payment)
        {
            _paymentDal.Delete(payment);
            return new SuccessResult();
        }

        public IDataResult<Payment> FindByID(int Id)
        {
            Payment p = new Payment();
            if (_paymentDal.GetAll().Any(c=>c.PaymentId==Id))
            {
                p = _paymentDal.GetAll().FirstOrDefault(c => c.PaymentId == Id);
            }
            else Console.WriteLine(Messages.NotExist + "payment");
            return new SuccessDataResult<Payment>(p);
        }

        public IDataResult<Payment> Get(Payment payment)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<Payment>> GetAll()
        {
            return new SuccessDataResult<List<Payment>>( _paymentDal.GetAll());
        }

        public IResult GetList(List<Payment> list)
        {
            throw new NotImplementedException();
        }

        public IResult Update(Payment payment)
        {
            throw new NotImplementedException();
        }
        private IResult CheckIsCardValid(string cardNumber,DateTime expirationDate,string securityCode)
        {
            if (!_creditCardService.GetAll().Data.Any(
               c=> c.CreditCardNumber==cardNumber &&
               c.ExpirationDate==expirationDate &&
               c.SecurityCode==securityCode))
            {
                return new ErrorResult(Messages.CardInvalid);
            }
            return new SuccessResult();
        }
    }
}
