using Microsoft.EntityFrameworkCore;
using TEMS.Business.Models;
using TEMS.Bussiness.Models;

namespace TEMS.Data.DBContext
{
    public class EventDbContext : DbContext
    {
        public EventDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<City> City { get; set; }

        public DbSet<Events> Event { get; set; }

        public DbSet<Speakers> Speaker { get; set; }

        public DbSet<TalkDetails> TalkDetails { get; set; }

        public DbSet<Venue> Venue { get; set; }

        public DbSet<EventVenueXRef> EventVenueXRef { get; set; }
    }
}
