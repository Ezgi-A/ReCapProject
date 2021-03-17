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
    public class ColorManager : IColorService
    {
        IColorDal _ıcolorDal;

        public ColorManager(IColorDal ıcolorDal)
        {
            _ıcolorDal = ıcolorDal;
        }

        public IResult Add(Color color)
        {
            if (color.ColorName.Length<2)
            {
                return new ErrorResult(Messages.ColorInvalid);
            }

            _ıcolorDal.Add(color);
            return new SuccessResult(Messages.ColorAdding);
        }

        public IResult Delete(Color color)
        {
            _ıcolorDal.Delete(color);
            return new SuccessResult(Messages.ColorDeleting);
        }

        public IDataResult<List<Color>> GetAll()
        {
            //if (DateTime.Now.Hour==22)
            //{
            //    return new ErrorDataResult<List<Color>>(Messages.Maintenance);
            //}
            
            return new SuccessDataResult<List<Color>>(_ıcolorDal.GetAll(),Messages.ColorListing);
        }

        public IDataResult<Color> GetById(int Colorid)
        {
            return new SuccessDataResult<Color>(_ıcolorDal.Get(co => co.ColorId == Colorid));
        }

        public IResult Update(Color color)
        {
            _ıcolorDal.Update(color);
            return new SuccessResult(Messages.ColorUpdating);
        }
    }
}
