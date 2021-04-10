using Business.Abstract;
using Business.Constants;
using Core.Utilities.BusinessRules;
using Core.Utilities.FileHelper;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;
        ICarService _carService;

        private readonly string DefaultImage = "default.jpg";
        public CarImageManager(ICarImageDal carImageDal, ICarService carService)
        {
            _carImageDal = carImageDal;
            _carService = carService;
        }
        public IResult Add(IFormFile image, CarImage img)
        {
            IResult result = BusinessRules.Run(CheckIfCarIsExists(img.CarId),
                                           CheckIfFileExtensionValid(image.FileName),
                                           CheckIfImageNumberLimitForCar(img.CarId));

            if (result != null)
            {
                return result;
            }
            img.ImagePath = FileHelper.Add(image);
            img.Date = DateTime.Now;
            _carImageDal.Add(img);
            return new SuccessResult("Image" + Messages.CarImageAdded);
        }

     

        public IResult Update(IFormFile image, CarImage img)
        {
            IResult result = BusinessRules.Run(CheckIfImageIsExists(img.ImageId),
                                           CheckIfFileExtensionValid(image.FileName));
            if (result != null)
            {
                return result;
            }
            var carImg = _carImageDal.Get(x => x.ImageId == img.ImageId);
            carImg.Date = DateTime.Now;
            carImg.ImagePath = FileHelper.Add(image);
            FileHelper.Delete(img.ImagePath);
            _carImageDal.Update(carImg);
            return new SuccessResult("Image" + Messages.CarImageUpdated);
        }

        public IResult Delete(CarImage img)
        {
            IResult result = BusinessRules.Run(CheckIfImagePathIsExists(img.ImagePath));
            if (result != null)
            {
                return result;
            }

            _carImageDal.Delete(img);
            FileHelper.Delete(img.ImagePath);
            return new SuccessResult("Image" + Messages.CarImageDeleted);
        }

        public IDataResult<CarImage> FindByID(int Id)
        {
            CarImage img = new CarImage();
            if (_carImageDal.GetAll().Any(x => x.ImageId == Id))
            {
                img = _carImageDal.GetAll().FirstOrDefault(x => x.ImageId == Id);
            }
            else Console.WriteLine("No such car image was found.");
            return new SuccessDataResult<CarImage>(img);
        }

        public IDataResult<CarImage> Get(CarImage img)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(x => x.ImageId == img.ImageId));
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }

        public IResult GetList(List<CarImage> list)
        {
            Console.WriteLine("\n------- Car Image List -------");

            foreach (var img in list)
            {
                Console.WriteLine("{0}- Car ID: {1}\n    Image Path: {2}\n    CratedAt: {3}\n", img.ImageId, img.CarId, img.ImagePath, img.Date);
            }
            return new SuccessResult();
        }

        public IDataResult<List<CarImage>> GetCarListByCarID(int carID)
        {
            if (!_carImageDal.GetAll().Any(x => x.CarId == carID))
            {
                List<CarImage> img = new List<CarImage>
                {
                    new CarImage
                    {
                        CarId = carID,
                        ImagePath = DefaultImage
                    }
                };
                return new SuccessDataResult<List<CarImage>>(img);
            }
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(x => x.CarId == carID));
        }

        private IResult CheckIfCarIsExists(int carId)
        {
            if (!_carService.GetAll().Data.Any(x => x.CarId == carId))
            {
                return new ErrorResult(Messages.NotExist + "car");
            }
            return new SuccessResult();
        }

        private IResult CheckIfFileExtensionValid(string file)
        {
            if (!Regex.IsMatch(file, @"([A-Za-z0-9\-]+)\.(png|PNG|gif|GIF|jpg|JPG|jpeg|JPEG)"))
            {
                return new ErrorResult(Messages.CarImageInvalid);
            }

            return new SuccessResult();
        }

        private IResult CheckIfImagePathIsExists(string path)
        {
            if (!_carImageDal.GetAll().Any(x => x.ImagePath == path))
            {
                return new ErrorResult(Messages.NotExistImage + "image");
            }

            return new SuccessResult();
        }

        private IResult CheckIfImageNumberLimitForCar(int carId)
        {
            if (_carImageDal.GetAll(x => x.CarId == carId).Count == 5)
            {
                return new ErrorResult(Messages.CarImagesLimitExceded);
            }
            return new SuccessResult();
        }

        private IResult CheckIfImageIsExists(int Id)
        {
            if (!_carImageDal.GetAll().Any(x => x.ImageId == Id))
            {
                return new ErrorResult(Messages.NotExist + "image");
            }
            return new SuccessResult();
        }
    }
}
