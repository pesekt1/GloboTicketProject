using System;

namespace GloboTicket.Promotion.Venues
{
    public class VenueInfo
    {
        public Guid VenueGuid { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public long LastModifiedTicks { get; set; }
    }
}