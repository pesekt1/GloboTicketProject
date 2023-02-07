using GloboTicket.Promotion.Acts;
using GloboTicket.Promotion.Contents;
using GloboTicket.Promotion.Shows;
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

        public DbSet<Act> Act { get; set; }
        public DbSet<Venue> Venue { get; set; }
        public DbSet<Show> Show { get; set; }
        public DbSet<Content> Content { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Act>()
                .HasAlternateKey(act => new { act.ActGuid });

            modelBuilder.Entity<ActRemoved>()
                .HasAlternateKey(actRemoved => new { actRemoved.ActId, actRemoved.RemovedDate });

            modelBuilder.Entity<ActDescription>()
                .HasAlternateKey(actDescription => new { actDescription.ActId, actDescription.ModifiedDate });

            modelBuilder.Entity<Venue>()
                .HasAlternateKey(venue => new { venue.VenueGuid });

            modelBuilder.Entity<Show>()
                .HasAlternateKey(show => new { show.ActId, show.VenueId, show.StartTime });

            modelBuilder.Entity<ShowCancelled>()
                .HasAlternateKey(showCancelled => new { showCancelled.ShowId, showCancelled.CancelledDate });

            modelBuilder.Entity<Content>()
                .HasKey(content => content.Hash);
            modelBuilder.Entity<Content>()
                .Property(content => content.Binary)
                .IsRequired();
        }

        public async Task<Act> GetOrInsertAct(Guid actGuid)
        {
            var act = Act
                .Include(act => act.Descriptions)
                .Where(act => act.ActGuid == actGuid)
                .SingleOrDefault();
            if (act == null)
            {
                act = new Act
                {
                    ActGuid = actGuid
                };
                await AddAsync(act);
            }

            return act;
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

        public async Task<Show> GetOrInsertShow(Guid actGuid, Guid venueGuid, DateTimeOffset startTime)
        {
            var show = Show
                .Where(show =>
                    show.Act.ActGuid == actGuid &&
                    show.Venue.VenueGuid == venueGuid &&
                    show.StartTime == startTime)
                .SingleOrDefault();
            if (show == null)
            {
                show = new Show
                {
                    Act = await GetOrInsertAct(actGuid),
                    Venue = await GetOrInsertVenue(venueGuid),
                    StartTime = startTime
                };
                await base.AddAsync(show);
            }

            return show;
        }
    }
}
