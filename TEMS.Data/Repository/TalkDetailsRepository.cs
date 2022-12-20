using TEMS.Business.Interface;
using TEMS.Business.Models;
using TEMS.Data.DBContext;

namespace TEMS.Data.Repository
{
    public class TalkDetailsRepository : ITalkDetailsRepository
    {
        private readonly EventDbContext dbContext;
        public TalkDetailsRepository(EventDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public TalkDetails AddTalkDetails(TalkDetails item)
        {
            this.dbContext.Add(item);
            this.dbContext.SaveChanges();
            return item;
        }

        public List<TalkDetails> GetSpeakersTalkDetailsById(int speakerId)
        {
                return this.dbContext.TalkDetails.Where(x => x.SpeakerId == speakerId).ToList();
        }
    }
}
