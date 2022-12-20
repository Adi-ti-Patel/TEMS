using System.ComponentModel.DataAnnotations;

namespace TEMS.Client.InputModel
{
    public class SpeakerInputModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string TwitterHandle { get; set; }

        [Required]
        public string LinkedInUrl { get; set; }

        [Required]
        public IFormFile Photo { get; set; }
    }
}
