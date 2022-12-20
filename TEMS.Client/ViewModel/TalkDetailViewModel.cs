namespace TEMS.Client.ViewModel
{
    public class TalkDetailViewModel
    {
        public int Id { get; set; }

        public int SpeakerId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTimeOffset StartTime { get; set; }

        public DateTimeOffset EndTime { get; set; }
    }
}
