using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GloboTicket.Promotion.Venues
{
    public class Venue
    {
        public int VenueId { get; set; }
        [Required]
        public Guid VenueGuid { get; set; }

        public ICollection<VenueDescription> Descriptions { get; set; } = new List<VenueDescription>();
        public ICollection<VenueRemoved> Removed { get; set; } = new List<VenueRemoved>();

        // This data to be moved to a new table.
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string City { get; set; }
    }
}