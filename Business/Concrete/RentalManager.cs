using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Entities.DTOs;
using Core.Utilities.BusinessRules;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;
        ICarDal _carDal;
        ICustomerDal _customerDal;
        public RentalManager(IRentalDal rentaldal,ICarDal cardal,ICustomerDal customerdal)
        {
            _rentalDal = rentaldal;
            _carDal = cardal;
            _customerDal = customerdal;
        }

        public IResult Add(Rental rental)
        {
            var result = BusinessRules.Run(CheckFindex(rental.CustomerId, rental.CarId));

            if (result!=null)
            {
                return result;
            }
            _rentalDal.Add(rental);
            return new SuccessResult(Messages.RentalAdding);
        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.RentalDeleting);
        }

        public IDataResult<List<Rental>> GetAll()
        {
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<List<Rental>>(_rentalDal.GetAll(), Messages.Maintenance);
            }
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(), Messages.RentalListing);
        }

        public IDataResult<List<RentalDetailDto>> GetRentalDetails()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails());
        }

        public IDataResult<Rental> GetById(int Rentalid)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(r=>r.RentalId==Rentalid));
        }

        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult(Messages.RentalUpdating);
        }

        public IResult CheckFindex(int customerId, int carId)
        {
            var car = _carDal.Get(c => c.CarId == carId);
            var customer = _customerDal.Get(cu => cu.CustomerId == customerId);

            var carFindex = car.MinimumFindex;
            var customerFindex = customer.Findex;

            if (customerFindex >= carFindex)
            {
                return new SuccessResult();
            }
            return new ErrorResult(Messages.FindexNotEnough);
            
        }
    }
}
