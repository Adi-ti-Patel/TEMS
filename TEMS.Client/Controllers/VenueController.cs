using Microsoft.AspNetCore.Mvc;
using TEMS.Business.Interface;
using TEMS.Business.Models;
using TEMS.Client.InputModel;

namespace Event.Client.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class VenueController : ControllerBase
    {
        private readonly IVenueRepository venueRepository;
        public VenueController(IVenueRepository repository)
        {
            this.venueRepository = repository;
        }

        [HttpGet("events/{id}/venues")]
        public IActionResult GetAllVenueOfSpecificEvent(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                return this.Ok(this.venueRepository.GetAllVenueOfSpecificEvent(id));
            }
            catch
            {
                return NotFound();
                //return this.Problem("Unable to serve request. Please contact administrator", null, 500);
            }

        }

        [HttpGet("events/{eventId}/venue/{venueId}")]
        public IActionResult GetSpecificVenueDetails(int eventId, int venueId)
        {
            try
            {
                if (eventId == 0 || venueId == 0)
                {
                    return BadRequest();
                }
                return this.Ok(this.venueRepository.GetSpecificVenueDetails(eventId, venueId));
            }
            catch
            {
                return NotFound();
                //return this.Problem("Unable to serve request. Please contact administrator", null, 500);
            }
        }


        //Extra Method

        [HttpGet("Venue/{cityId}")]
        public IActionResult GetVenueByCityId(int cityId)
        {
            try
            {
                if (cityId == 0)
                {
                    return BadRequest();
                }
                return this.Ok(this.venueRepository.GetVenueByCityId(cityId));
            }
            catch
            {
                return NotFound();
                //return this.Problem("Unable to serve request. Please contact administrator", null, 500);
            }
        }

        //[HttpPost("Venue")]
        //public IActionResult AddVenue(Venue item)
        //{
        //    try
        //    {
        //        if (item == null)
        //        {
        //            return NotFound();
        //        }
        //        return Ok(this.venueRepository.AddVenue(item));
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }
        //}

        [HttpPost("Venue")]
        public IActionResult Post([FromForm] VenueInputModel newVenue, [FromServices] IWebHostEnvironment environment)
        {
            try
            {
                if (newVenue != null)
                {
                    //SavePostImageAsync(newTalkDetails.Pic, environment);
                    Venue venue = ConvertInputModelToDbModel(newVenue);

                    venueRepository.AddVenue(venue);
                    return CreatedAtRoute(new { id = venue.Id }, venue);
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

        private static Venue ConvertInputModelToDbModel(VenueInputModel venueInputModel)
        {
            Venue venue = new()
            {
                Name = venueInputModel.Name,
                Website = venueInputModel.Website,
                CityId = venueInputModel.CityId,
                Contact = venueInputModel.Contact,

            };
            return venue;
        }
    }
}
