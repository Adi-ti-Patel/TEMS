using TEMS.Business.Models;

namespace TEMS.Business.Interface
{
    public interface ITalkDetailsRepository
    {
        List<TalkDetails> GetSpeakersTalkDetailsById(int speakerId);

        TalkDetails AddTalkDetails(TalkDetails item);
    }
}
