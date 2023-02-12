using System;

namespace GloboTicket.Promotion.Messages.Venues
{
    public class VenueDescriptionChanged
    {
        public Guid venueGuid { get; set; }
        public VenueDescriptionRepresentation description { get; set; }
    }
}
