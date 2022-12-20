using TEMS.Business.Interface;
using TEMS.Business.Models;
using TEMS.Data.DBContext;

namespace TEMS.Data.Repository
{
    public class VenueRepository : IVenueRepository
    {
        private readonly EventDbContext dbContext;

        public VenueRepository(EventDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<Venue> GetSpecificVenueDetails(int eventId, int venueId)
        {
                return (from v in dbContext.Venue
                        join e in dbContext.Event
                        on v.CityId equals e.CityId
                        where e.Id == eventId && v.Id == venueId && e.CityId == v.CityId
                        select v).ToList();
        }

        public List<Venue> GetAllVenueOfSpecificEvent(int eventId)
        {
            //return this.dbContext.Venue.Join(dbContext.EventVenueXRef, v => v.Id, x => x.VenueId,
            //(v, x) => new { v, x }).Where(x => x.x.EventId == eventId).Select(x => x.v).ToList();

                return (from v in dbContext.Venue
                        join e in dbContext.Event
                        on v.CityId equals e.CityId
                        where e.Id == eventId && e.CityId == v.CityId
                        select v).ToList();
        }

        public List<Venue> GetVenueByCityId(int cityId)
        {
            //return (from v in dbContext.Venue
            //        join c in dbContext.Cities
            //        on v.CityId equals cityId
            //        where v.CityId == c.Id
            //        select v, c.name).ToList();

                return this.dbContext.Venue.Where(x => x.CityId == cityId).ToList();
        }

        public Venue AddVenue(Venue item)
        {
            this.dbContext.Add(item);
            this.dbContext.SaveChanges();
            return item;
        }
    }
}
