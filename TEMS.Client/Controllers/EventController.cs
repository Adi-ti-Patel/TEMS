using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TEMS.Business.Interface;
using TEMS.Business.Models;
using TEMS.Client.InputModel;
using TEMS.Client.ViewModel;

namespace TEMS.Client.Controllers
{
    [EnableCors()]
    [ApiController]
    [Route("api/v1")]
    public class EventController : ControllerBase
    {
        private readonly IEventsRepository eventsRepository;

        public EventController(IEventsRepository repository)
        {
            this.eventsRepository = repository;
        }


        [HttpGet("events/{id}")]
        public IActionResult GetEventById (int id)
        {
            try
            {
                if(id == null)
                {
                    return NotFound();
                }
                return this.Ok(this.eventsRepository.GetEventById(id));
            }
            catch
            {
                return BadRequest();
                //return this.Problem("Unable to serve request. Please contact administrator", null, 400);
            }
        }

        [HttpGet("events/{month}/{year}")]
        public IActionResult GetEventsByMonth (int month, int year)
        {
            try
            {
                if(month == null || year == null)
                {
                    return NotFound();
                }
                return this.Ok(this.eventsRepository.GetEventByMonth(month, year));
            }
            catch
            {
                return BadRequest();
                //return this.Problem("Unable to serve request. Please contact administrator", null, 500);
            }
        }


        [HttpGet("events/completed")]
        public IActionResult GetAllCompletedEvents()
        {
            List<Events> events = eventsRepository.GetAllCompletedEvent();
            try
            {
                if(events == null)
                {
                    return NotFound();
                }
                return this.Ok(events);
            }
            catch
            {
                return BadRequest();
                //return this.Problem("Unable to serve request. Please contact administrator", null, 500);
            }
        }


        [HttpGet("events/getAll")]
        public  ActionResult<IEnumerable<EventViewModel>> GetAll()
        {
            try
            {
                IEnumerable<Events> events = eventsRepository.GetAll();

                if (events == null)
                {
                    return NotFound();
                }
                IEnumerable<EventViewModel> eventViewModels = convertDbModelToViewModel(events);

                return Ok(eventViewModels);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("events/{mon}/{yr}/notCompletedEvents")]
        public ActionResult<IEnumerable<EventViewModel>> GetAllNotCompletedEvents(int mon, int yr)
        {
            try
            {
                IEnumerable<Events> events = eventsRepository.GetAllNotCompletedEvent(mon,yr);

                if (events == null)
                {
                    return NotFound();
                }
                IEnumerable<EventViewModel> eventViewModels = convertDbModelToViewModel(events);

                return Ok(eventViewModels);
            }
            catch
            {
                return BadRequest();
            }
        }

        private static IEnumerable<EventViewModel> convertDbModelToViewModel(IEnumerable<Events> events)
        {
            ICollection<EventViewModel> eventViewModels = new List<EventViewModel>();

            foreach (var item in events)
            {
                eventViewModels.Add(new EventViewModel { Id = item.Id, Name = item.Name, Pic = item.Pic, StartDate = item.StartDate, EndDate = item.EndDate, CityId = item.CityId});
            }
                return eventViewModels;

        }

        //[HttpPost("Event")]
        //public IActionResult AddEvent(Events item)
        //{
        //    try
        //    {
        //        if (item == null)
        //        {
        //            return NotFound();
        //        }
        //        return Ok(this.eventsRepository.AddEvent(item));
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }
        //}

        [HttpPost("Event")]
        public IActionResult Post([FromForm] EventInputModel newEvent, [FromServices] IWebHostEnvironment environment)
        {
            try
            {
                if (newEvent != null)
                {
                    SavePostImageAsync(newEvent.Pic, environment);
                    Events events = ConvertInputModelToDbModel(newEvent);

                    eventsRepository.AddEvent(events);
                    return CreatedAtRoute(new { id = events.Id }, events);
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

        private static Events ConvertInputModelToDbModel(EventInputModel eventsInputModel)
        {
            Events events = new()
            {
                CityId = eventsInputModel.CityId,
                StartDate = eventsInputModel.StartDate,
                EndDate = eventsInputModel.EndDate,
                IsCompleted = eventsInputModel.IsCompleted,
                Name = eventsInputModel.Name,
                Pic = eventsInputModel.Pic.FileName,
            };
            return events;
        }

        private static async Task SavePostImageAsync(IFormFile newImage, IWebHostEnvironment environment)
        {
            if (newImage != null)
            {
                var uploadFileName = newImage.FileName;
                var rootPath = Path.Combine(environment.WebRootPath, "EventsImages");
                var filePath = Path.Combine(rootPath, uploadFileName);
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                await newImage.CopyToAsync(new FileStream(filePath, FileMode.Create));
            }
        }

    }
}
