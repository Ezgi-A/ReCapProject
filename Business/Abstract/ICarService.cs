using Core.Utilities.Results;
using Entities;
using Entities.Concrete;
using System;
using Entities.DTOs;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace Business.Abstract
{
    public interface ICarService
    {
        IDataResult<List<Car>> GetAll();

        IDataResult<List<CarDetailDto>> GetCarDetails(Expression<Func<Car, bool>> filter = null);
        

        IDataResult<List<Car>> GetCarByBrandAndColor(int brandId, int colorId);



        IDataResult<Car> GetById(int Carid);

        IResult Add(Car car);

        IResult Delete(Car car);
        IResult Update(Car car);

        IResult TransactionalOperation(Car car);


    }
}
