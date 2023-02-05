using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GloboTicket.Promotion.Data;

namespace GloboTicket.Promotion.Venues
{
    public class VenueCommands
    {
        private readonly PromotionContext repository;

        public VenueCommands(PromotionContext repository)
        {
            this.repository = repository;
        }

        public async Task SaveVenue(VenueInfo venueInfo)
        {
            var venue = await repository.GetOrInsertVenue(venueInfo.VenueGuid);
            var lastVenueDescription = venue.Descriptions
                .OrderByDescending(description => description.ModifiedDate)
                .FirstOrDefault();

            if (lastVenueDescription == null ||
                lastVenueDescription.Name != venueInfo.Name ||
                lastVenueDescription.City != venueInfo.City)
            {

                var modifiedTicks = lastVenueDescription?.ModifiedDate.Ticks ?? 0;
                if (modifiedTicks != venueInfo.LastModifiedTicks)
                {
                    throw new DbUpdateConcurrencyException("A new update has occurred since you loaded the page. Please refresh and try again.");
                }

                await repository.AddAsync(new VenueDescription
                {
                    ModifiedDate = DateTime.UtcNow,
                    Venue = venue,
                    Name = venueInfo.Name,
                    City = venueInfo.City
                });
                await repository.SaveChangesAsync();
            }

        }

        public async Task DeleteVenue(Guid venueGuid)
        {
            var venue = await repository.GetOrInsertVenue(venueGuid);
            await repository.AddAsync(new VenueRemoved
            {
                Venue = venue,
                RemovedDate = DateTime.UtcNow
            });
            await repository.SaveChangesAsync();
        }
    }
}