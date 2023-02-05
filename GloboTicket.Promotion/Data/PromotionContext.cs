using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GloboTicket.Promotion.Venues;

namespace GloboTicket.Promotion.Data
{
    public class PromotionContext : DbContext
    {
        public PromotionContext (DbContextOptions<PromotionContext> options)
            : base(options)
        {
        }

        public DbSet<GloboTicket.Promotion.Venues.Venue> Venue { get; set; }
    }
}
