using Microsoft.AspNetCore.Mvc;
using TEMS.Business.Interface;
using TEMS.Business.Models;
using TEMS.Client.InputModel;
using TEMS.Client.ViewModel;

namespace TEMS.Client.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class AuthorController : ControllerBase
    {
        private readonly ISpeakersRepository speakerRepository;

       
        public AuthorController(ISpeakersRepository repository)
        {
            this.speakerRepository = repository;
        }

        [HttpGet("events/{id}/authors")]
        public IActionResult GetAllSpeakerOfSpecificEvent(int id)
        {
            try
            {
                if(id == null)
                {
                    return NotFound();
                }
                return this.Ok(this.speakerRepository.GetAllSpeakerBySpecificEvent(id));
            }
            catch
            {
                return BadRequest();
            }
           
        }

        [HttpGet("events/{eventId}/authors/{authorId}/talks")]
        public IActionResult GetTalkConductedBySpeakerForSpecificEvent(int eventId, int authorId)
        {
            try
            {
                if(eventId == null || authorId == null)
                {
                    return NotFound();
                }
                return this.Ok(this.speakerRepository.GetTalksConductedBySpeakerOfSpecificEvent(eventId, authorId));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("events/{eventId}/authors/{authorId}/talks/completed")]
        public IActionResult GetCompletedTalksBySpecificSpeaker(int eventId, int authorId)
        {
            try
            {
                if(eventId == null || authorId == null)
                {
                    return NotFound();
                }
                return this.Ok(this.speakerRepository.GetTalksCompletedBySpecificSpeaker(eventId, authorId));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("speaker/authorId")]
        public ActionResult<IEnumerable<SpeakerViewModel>> GetSpeakerById(int authorId)
        {
            try
            {
                IEnumerable<Speakers> speakers = speakerRepository.GetSpeakerById(authorId);

                if (speakers == null)
                {
                    return NotFound();
                }
                IEnumerable<SpeakerViewModel> speakerViewModels = convertDbModelToViewModel(speakers);

                return Ok(speakerViewModels);
            }
            catch
            {
                return BadRequest();
            }
        }

        private static IEnumerable<SpeakerViewModel> convertDbModelToViewModel(IEnumerable<Speakers> speakers)
        {
            ICollection<SpeakerViewModel> speakerViewModels = new List<SpeakerViewModel>();

            foreach (var item in speakers)
            {
                speakerViewModels.Add(new SpeakerViewModel { Id = item.Id, Email = item.Email, Name = item.Name, Photo =item.Photo});
            }
            return speakerViewModels;
            
        }

        [HttpPost("Speaker")]
        public  IActionResult Post([FromForm] SpeakerInputModel newSpeaker, [FromServices] IWebHostEnvironment environment)
        {
            try
            {
                if (newSpeaker != null)
                {
                     SavePostImageAsync(newSpeaker.Photo, environment);
                    Speakers speaker = ConvertInputModelToDbModel(newSpeaker);



                    speakerRepository.AddSpeaker(speaker);
                    return CreatedAtRoute(new { id = speaker.Id }, speaker);
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

        private static Speakers ConvertInputModelToDbModel(SpeakerInputModel speakerInputModel)
        {
            Speakers speaker = new()
            {
                Name = speakerInputModel.Name,
                Email = speakerInputModel.Email,
                TwitterHandle = speakerInputModel.TwitterHandle,
                Photo = speakerInputModel.Photo.FileName,
                LinkedInUrl = speakerInputModel.LinkedInUrl,
            };
            return speaker;
        }

        private static async Task SavePostImageAsync(IFormFile newImage, IWebHostEnvironment environment)
        {
            if (newImage != null)
            {
                var uploadFileName = newImage.FileName;
                var rootPath = Path.Combine(environment.WebRootPath, "SpeakersImages");
                var filePath = Path.Combine(rootPath, uploadFileName);
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                await newImage.CopyToAsync(new FileStream(filePath, FileMode.Create));
            }
        }

    }
}
