using Microsoft.AspNetCore.Mvc;
using TEMS.Business.Interface;
using TEMS.Business.Models;
using TEMS.Client.InputModel;

namespace TEMS.Client.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class CityController : ControllerBase
    {
        private readonly ICitiesRepository citiesRepository;
        public CityController(ICitiesRepository repository)
        {
            this.citiesRepository = repository;
        }

        //[HttpPost("City")]
        //public IActionResult AddCity(City item)
        //{
        //    try
        //    {
        //        if(item == null)
        //        {
        //            return NotFound();
        //        }
        //        return Ok(this.citiesRepository.AddCity(item));
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }
        //}

        [HttpPost("City")]
        public IActionResult Post([FromForm] CityInputModel newCity, [FromServices] IWebHostEnvironment environment)
        {
            try
            {
                if (newCity != null)
                {
                    //SavePostImageAsync(newCity.Photo, environment);
                    City city = ConvertInputModelToDbModel(newCity);

                    citiesRepository.AddCity(city);
                    return CreatedAtRoute(new { id = city.Id }, city);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        private static City ConvertInputModelToDbModel(CityInputModel cityInputModel)
        {
            City city = new()
            {
                Name = cityInputModel.Name,
                
            };
            return city;
        }



    }
}
