using TEMS.Business.Interface;
using TEMS.Business.Models;
using TEMS.Data.DBContext;

namespace TEMS.Data.Repository
{
    public class SpeakersRepository : ISpeakersRepository
    {
        private readonly EventDbContext dbContext;
        public SpeakersRepository(EventDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Speakers AddSpeaker(Speakers item)
        {
            this.dbContext.Add(item);
            this.dbContext.SaveChanges();
            return item;
        }

        public List<Speakers> GetAllSpeakerBySpecificEvent(int eventId)
        {
                return (from ev in dbContext.EventVenueXRef
                        join e in dbContext.Event
                        on ev.EventId equals e.Id
                        join s in dbContext.Speaker
                        on ev.SpeakerId equals s.Id
                        where e.Id == eventId
                        select s).ToList();
        }

        public List<Speakers> GetSpeakerById(int authorId)
        {
                return this.dbContext.Speaker.Where(x => x.Id == authorId).ToList();
        }

        public List<TalkDetails> GetTalksCompletedBySpecificSpeaker(int eventId, int speakerId)
        {
                return (from ev in dbContext.EventVenueXRef
                        join e in dbContext.Event
                        on ev.EventId equals e.Id
                        join t in dbContext.TalkDetails
                        on ev.SpeakerId equals t.SpeakerId
                        where e.Id == eventId && ev.SpeakerId == speakerId
                        select t).ToList();
        }

        public List<TalkDetails> GetTalksConductedBySpeakerOfSpecificEvent(int eventId, int speakerId)
        {
                return (from ev in dbContext.EventVenueXRef
                        join e in dbContext.Event
                        on ev.EventId equals e.Id
                        join t in dbContext.TalkDetails
                        on ev.SpeakerId equals t.SpeakerId
                        where e.Id == eventId && ev.SpeakerId == speakerId && e.IsCompleted == true
                        select t).ToList();
        }

    }
}
