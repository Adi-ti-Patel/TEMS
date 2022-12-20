using TEMS.Business.Models;

namespace TEMS.Business.Interface
{
    public interface IEventsRepository
    {
        Events GetEventById(int id);

        List<Events> GetEventByMonth(int month, int year);

        List<Events> GetAllCompletedEvent();

        List<Events> GetAll();

        List<Events> GetAllNotCompletedEvent(int mon, int yr);

        Events AddEvent(Events item);
    }
}
