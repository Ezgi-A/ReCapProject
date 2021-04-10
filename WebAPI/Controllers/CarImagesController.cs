using Business.Abstract;
using Core.Utilities.FileHelper;
using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarImagesController : ControllerBase
    {

        ICarImageService _carImageService2;
        public CarImagesController(ICarImageService carImageService2)
        {
            _carImageService2 = carImageService2;
        }

        [HttpPost("add")]
        public IActionResult Add([FromForm] IFormFile image, [FromForm] CarImage img)
        {
            var result = _carImageService2.Add(image, img);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete([FromForm] CarImage img)
        {
            var result = _carImageService2.Delete(img);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update([FromForm] IFormFile image, [FromForm] CarImage img)
        {
            var result = _carImageService2.Update(image, img);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getcarlistbycarid")]
        public IActionResult GetCarListByCarID(int carId)
        {
            var result = _carImageService2.GetCarListByCarID(carId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _carImageService2.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("getcarimagebyid")]
        public IActionResult Get([FromForm] int Id)
        {
            var result = _carImageService2.FindByID(Id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        //------------------------------------------------------------------
        //ICarImageService _carImageService;



        //[HttpGet("getall")]
        //public IActionResult GetAll()
        //{
        //    var result = _carImageService.GetAll();
        //    if (result.Success)
        //    {
        //        return Ok(result);
        //    }
        //    return BadRequest(result);
        //}

        //[HttpGet("getbyid")]
        //public IActionResult GetById(int id)
        //{
        //    var result = _carImageService.GetById(id);
        //    if (result.Success)
        //    {
        //        return Ok(result);
        //    }
        //    return BadRequest(result);
        //}



        //[HttpGet("checkifcarimagenull")]
        //public IActionResult CheckIfCarImageNull(int carId)
        //{
        //    var result = _carImageService2.CheckIfCarImageNull(carId);
        //    if (result.Success)
        //    {
        //        return Ok(result);
        //    }
        //    return BadRequest(result);
        //}

        //[HttpPost("add")]
        //public IActionResult Add([FromForm] CarImage carImage, [FromForm] IFormFile file)
        //{
        //    var result = _carImageService2.Add(carImage, file);
        //    if (result.Success)
        //    {
        //        return Ok(result);
        //    }
        //    return BadRequest(result);
        //}

        //[HttpPost("update")]
        //public IActionResult Update([FromForm] CarImage carImage, [FromForm] IFormFile file)
        //{
        //    var result = _carImageService.Update(carImage, file);
        //    if (result.Success)
        //    {
        //        return Ok(result);
        //    }
        //    return BadRequest(result);
        //}

        //[HttpPost("delete")]
        //public IActionResult Delete(CarImage carImage)
        //{
        //    var result = _carImageService.Delete(carImage);
        //    if (result.Success)
        //    {
        //        return Ok(result);
        //    }
        //    return BadRequest(result);
        //}


    }
}

