using System;
using System.Collections.Generic;
using GloboTicket.Promotion.Shows;

namespace GloboTicket.Promotion.Acts
{
    public class ActViewModel
    {
        public ActInfo Act { get; set; }
        public List<ShowInfo> Shows { get; set; }
    }
}
