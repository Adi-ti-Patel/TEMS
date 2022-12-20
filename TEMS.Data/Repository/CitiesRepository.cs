using TEMS.Business.Interface;
using TEMS.Business.Models;
using TEMS.Data.DBContext;

namespace TEMS.Data.Repository
{
    public class CitiesRepository : ICitiesRepository
    {
        private readonly EventDbContext dbContext;
        public CitiesRepository(EventDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public City AddCity(City item)
        {

            this.dbContext.Add(item);
            this.dbContext.SaveChanges();
            return item;
        }
    }
}
