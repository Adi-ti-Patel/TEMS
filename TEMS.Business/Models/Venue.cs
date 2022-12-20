using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEMS.Business.Models
{
    public class Venue
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Website { get; set; }
        
        public int CityId { get; set; }

        public string Contact { get; set; }
    }
}
