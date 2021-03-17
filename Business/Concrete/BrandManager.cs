using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        IBrandDal _ıbrandDal;

        public BrandManager(IBrandDal ıbrandDal)
        {
            _ıbrandDal = ıbrandDal;
        }

        public IResult Add(Brand brand)
        {
            if (brand.BrandName.Length<2)
            {
                return new ErrorResult(Messages.BrandInvalid);
            }
            _ıbrandDal.Add(brand);
            return new SuccessResult(Messages.BrandAdding);
        }

        public IResult Delete(Brand brand)
        {
            _ıbrandDal.Delete(brand);
            return new SuccessResult(Messages.BrandDeleting);
        }

        public IDataResult<List<Brand>> GetAll()
        {
            //if (DateTime.Now.Hour == 22)
            //{
            //    return new ErrorDataResult<List<Brand>>(Messages.Maintenance);
            //}
            return new SuccessDataResult<List<Brand>> (_ıbrandDal.GetAll());
        }

        public IDataResult<Brand> GetById(int Brandid)
        {
            return new SuccessDataResult<Brand>( _ıbrandDal.Get(b => b.BrandId == Brandid));
        }

        public IResult Update(Brand brand)
        {
            _ıbrandDal.Update(brand);
            return new SuccessResult(Messages.BrandUpdating);
        }
    }
}
