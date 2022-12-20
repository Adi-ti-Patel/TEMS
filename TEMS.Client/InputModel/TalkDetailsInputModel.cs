using System.ComponentModel.DataAnnotations;

namespace TEMS.Client.InputModel
{
    public class TalkDetailsInputModel
    {
        [Required]
        public int SpeakerId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Room { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string TalkTime { get; set; }

        [Required]
        public DateTimeOffset StartTime { get; set; }

        [Required]
        public DateTimeOffset EndTime { get; set; }
    }
}
