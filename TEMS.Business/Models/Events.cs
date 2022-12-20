using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEMS.Business.Models
{
    public class Events
    {
        public int Id { get; set; }

        public int CityId { get; set; }

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset EndDate { get; set; }

        public bool IsCompleted { get; set; }

        public string Name { get; set; }

        public string Pic { get; set; }
    }
}
