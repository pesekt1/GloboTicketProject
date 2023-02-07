using System;

namespace GloboTicket.Promotion.Acts
{
    public class ActRemoved
    {
        public int ActRemovedId { get; set; }

        public int ActId { get; set; }
        public Act Act { get; set; }

        public DateTime RemovedDate { get; set; }
    }
}