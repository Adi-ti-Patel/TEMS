using TEMS.Business.Interface;
using TEMS.Business.Models;
using TEMS.Data.DBContext;

namespace TEMS.Data.Repository
{
    public class EventsRepository : IEventsRepository
    {
        private readonly EventDbContext dbContext;
        public EventsRepository(EventDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<Events> GetAllCompletedEvent()
        {
                return this.dbContext.Event.Where(x => x.IsCompleted == true).ToList();
        }

        public List<Events> GetAllEvents()
        { 
                return this.dbContext.Event.ToList();
        }

        public Events GetEventById(int id)
        {
                return dbContext.Event.FirstOrDefault(x => x.Id == id);
        }

        public List<Events> GetEventByMonth(int month, int year)
        {
                return this.dbContext.Event.Where(x => x.StartDate.Month == month && x.StartDate.Year == year).ToList();
        }

        public List<Events> GetAll()
        {
            return this.dbContext.Event.ToList();
        }

        public List<Events> GetAllNotCompletedEvent(int mon, int yr)
        {
            return this.dbContext.Event.Where(x => x.IsCompleted == false && x.StartDate.Month == mon && x.StartDate.Year == yr).ToList();
        }

        public Events AddEvent(Events item)
        {

            this.dbContext.Add(item);
            this.dbContext.SaveChanges();
            return item;
        }
    }
}
