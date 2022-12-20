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
    public class TalksDetailsController : ControllerBase
    {
        private readonly ITalkDetailsRepository talkDetailRepository;

        public TalksDetailsController(ITalkDetailsRepository talkDetailRepository)
        {
            this.talkDetailRepository = talkDetailRepository;
        }

        //[HttpGet("speaker/{speakerId}/talks")]
        //public IActionResult GetSpeakersTalkDetailsById(int speakerId)
        //{
        //    try
        //    {
        //        return this.Ok(this.talkDetailRepository.GetSpeakersTalkDetailsById(speakerId));
        //    }
        //    catch
        //    {
        //        return this.Problem("Unable to serve request. Please contact administrator", null, 500);
        //    }
        //}

        [HttpGet("speaker/{speakerId}/talks")]
        public ActionResult<IEnumerable<TalkDetailViewModel>> GetSpeakersTalkDetailsById(int speakerId)
        {
            try
            {
                IEnumerable<TalkDetails> talkDetail = talkDetailRepository.GetSpeakersTalkDetailsById(speakerId);

                if (talkDetail == null)
                {
                    return NotFound();
                }
                IEnumerable<TalkDetailViewModel> talkDetailViewModels = convertDbModelToViewModel(talkDetail);

                return Ok(talkDetailViewModels);
            }
            catch
            {
                return BadRequest();
            }
        }

        private static IEnumerable<TalkDetailViewModel> convertDbModelToViewModel(IEnumerable<TalkDetails> talkDetail)
        {
            ICollection<TalkDetailViewModel> talkDetailViewModels = new List<TalkDetailViewModel>();

            foreach (var item in talkDetail)
            {
                talkDetailViewModels.Add(new TalkDetailViewModel { Id = item.Id, Title = item.Title, Description = item.Description, EndTime = item.EndTime, StartTime = item.StartTime, SpeakerId = item.SpeakerId });
            }
            return talkDetailViewModels;

        }

        //[HttpPost("TalkDetails")]
        //public IActionResult AddTalkdetails(TalkDetails item)

        //{
        //    try
        //    {
        //        if (item == null)
        //        {
        //            return NotFound();
        //        }
        //        return Ok(this.talkDetailRepository.AddTalkDetails(item));
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }
        //}

        [HttpPost("TalkDetails")]
        public IActionResult Post([FromForm] TalkDetailsInputModel newTalkDetails, [FromServices] IWebHostEnvironment environment)
        {
            try
            {
                if (newTalkDetails != null)
                {
                    //SavePostImageAsync(newTalkDetails.Pic, environment);
                    TalkDetails talkDetails = ConvertInputModelToDbModel(newTalkDetails);

                    talkDetailRepository.AddTalkDetails(talkDetails);
                    return CreatedAtRoute(new { id = talkDetails.Id }, talkDetails);
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

        private static TalkDetails ConvertInputModelToDbModel(TalkDetailsInputModel talkDetailsInputModel)
        {
            TalkDetails talkDetails = new()
            {
                SpeakerId = talkDetailsInputModel.SpeakerId,
                Title = talkDetailsInputModel.Title,
                Room = talkDetailsInputModel.Room,
                Description = talkDetailsInputModel.Description,
                TalkTime = talkDetailsInputModel.TalkTime,
                StartTime = talkDetailsInputModel.StartTime,
                EndTime = talkDetailsInputModel.EndTime,
            };
            return talkDetails;
        }
    }
}
