using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities.DTOs;
using Business.BusinessAspects.Autofac;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Performance;
using System.Threading;
using System.Linq.Expressions;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        [SecuredOperation("car.add,admin")]
        [ValidationAspect(typeof(CarValidator))]
        
      
        public IResult Add(Car car)
        {
            _carDal.Add(car);
            return new SuccessResult(Messages.CarAdding);
        }

        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Messages.CarDeleting);
        }
        //[SecuredOperation("car.list")]
        [CacheAspect]
        [PerformanceAspect(4)]
        public IDataResult<List<Car>> GetAll()
        {
            //Thread.Sleep(4000);
            //if (DateTime.Now.Hour == 22)
            //{
            //    return new ErrorDataResult<List<Car>>(Messages.Maintenance);
            //}

            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.CarListing);

        }

        public IDataResult<Car> GetById(int Carid)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.CarId == Carid));
        }

        //public IDataResult<List<CarDetailDto>> GetCarDetails()
        //{
        //    return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails());
        //}
        public IDataResult<List<CarDetailDto>> GetCarDetails(Expression<Func<Car, bool>> filter = null)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(filter));

        }

        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult(Messages.CarUpdating);
        }
        [TransactionScopeAspect]
        public IResult TransactionalOperation(Car car)
        {


            //Add(car);
            //if (car.ModelYear< 1988)
            //{
            //    throw new Exception("Araba eski model!");
            //}

            //_carDal.Add(car);
            //return new SuccessResult(Messages.CarAdding);
            throw new NotImplementedException();
        }

        

        public IDataResult<List<CarDetailDto>> GetCarByBrandAndColor(int brandId, int colorId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(c => c.BrandId == brandId && c.ColorId == colorId));
        }

        public IDataResult<List<CarDetailDto>> GetCarDetailByCarId(int carId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(c => c.CarId == carId));
        }
    }
}
