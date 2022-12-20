using TEMS.Business.Models;

namespace TEMS.Business.Interface
{
    public interface ISpeakersRepository 
    {
        List<Speakers> GetAllSpeakerBySpecificEvent(int eventId);

        List<TalkDetails> GetTalksConductedBySpeakerOfSpecificEvent(int eventId, int speakerId);

        List<TalkDetails> GetTalksCompletedBySpecificSpeaker(int eventId, int speakerId);

        List<Speakers> GetSpeakerById(int authorId);

        Speakers AddSpeaker(Speakers item);
    }
}
