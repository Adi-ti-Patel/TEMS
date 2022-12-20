using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEMS.Bussiness.Models
{
    public class EventVenueXRef
    {
        public int Id { get; set; }

        public int EventId { get; set; }

        public int SpeakerId { get; set; }
    }
}
