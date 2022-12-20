using System.ComponentModel.DataAnnotations;

namespace TEMS.Client.InputModel
{
    public class EventInputModel
    {
        [Required]
        public int CityId { get; set; }

        [Required]
        public DateTimeOffset StartDate { get; set; }

        [Required]
        public DateTimeOffset EndDate { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public IFormFile Pic { get; set; }
    }
}
