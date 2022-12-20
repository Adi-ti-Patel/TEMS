using System.ComponentModel.DataAnnotations;

namespace TEMS.Client.InputModel
{
    public class VenueInputModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Website { get; set; }

        [Required]
        public int CityId { get; set; }

        [Required]
        public string Contact { get; set; }
    }
}
