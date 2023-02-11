using System;
using System.Collections.Generic;
using GloboTicket.Promotion.Acts;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GloboTicket.Promotion.Shows
{
    public class CreateShowViewModel
    {
        public ActInfo Act { get; set; }
        public List<SelectListItem> Venues { get; set; }
        public Guid Venue { get; set; }
        public DateTime StartTime { get; set; }
    }
}
