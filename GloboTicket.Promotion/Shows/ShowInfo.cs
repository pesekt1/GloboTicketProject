using System;
using GloboTicket.Promotion.Venues;

namespace GloboTicket.Promotion.Shows
{
    public class ShowInfo
    {
        public Guid ActGuid { get; set; }
        public VenueInfo Venue { get; set; }
        public DateTimeOffset StartTime { get; set; }
    }
}