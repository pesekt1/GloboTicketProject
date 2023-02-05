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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //configuring VenueGuid property
            modelBuilder.Entity<Venue>()
                .HasAlternateKey(v => new { v.VenueGuid });
            //autogenerating the guid for the existing records
            //modelBuilder.Entity<Venue>().Property(v => v.VenueGuid)
           //.HasDefaultValueSql("NEWID()");
        }

    }
}
