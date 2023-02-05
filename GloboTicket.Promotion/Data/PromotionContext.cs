using GloboTicket.Promotion.Venues;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

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
            modelBuilder.Entity<Venue>()
                .HasAlternateKey(v => new { v.VenueGuid });
        }

        public async Task<Venue> GetOrInsertVenue(Guid venueGuid)
        {
            var venue = Venue
                .Include(venue => venue.Descriptions)
                .Where(venue => venue.VenueGuid == venueGuid)
                .SingleOrDefault();
            if (venue == null)
            {
                venue = new Venue
                {
                    VenueGuid = venueGuid
                };
                await AddAsync(venue);
            }

            return venue;
        }
    }
}
