using System;

namespace GloboTicket.Promotion.Messages.Acts
{
    public class ActDescriptionChanged
    {
        public Guid actGuid { get; set; }
        public ActDescriptionRepresentation description { get; set; }
    }
}
