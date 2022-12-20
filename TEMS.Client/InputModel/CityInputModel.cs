using System.ComponentModel.DataAnnotations;

namespace TEMS.Client.InputModel
{
    public class CityInputModel
    {
        [Required]
        public string Name { get; set; }
    }
}
