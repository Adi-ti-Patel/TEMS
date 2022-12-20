namespace TEMS.Client.ViewModel
{
    public class EventViewModel
    {
        public int Id { get; set; }

        public int CityId { get; set; }

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset EndDate { get; set; }

        public string Name { get; set; }

        public string Pic { get; set; }
    }
}
