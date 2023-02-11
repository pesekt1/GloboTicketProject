using System;

namespace GloboTicket.Promotion.Messages.Acts
{
    public class ActRepresentation
    {
        public Guid actGuid { get; set; }
        public ActDescriptionRepresentation description { get; set; }
    }
}