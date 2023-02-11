using System;
using GloboTicket.Promotion.Messages.Acts;
using GloboTicket.Promotion.Messages.Venues;

namespace GloboTicket.Promotion.Messages.Shows
{
    public class ShowAdded
    {
        public ActRepresentation act { get; set; }
        public VenueRepresentation venue { get; set; }
        public ShowRepresentation show { get; set; }
    }
}
