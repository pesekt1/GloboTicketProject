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

        public async Task SaveVenue(VenueInfo venueModel)
        {
            var venue = await repository.GetOrInsertVenue(venueModel.VenueGuid);
            await repository.AddAsync(new VenueDescription
            {
                ModifiedDate = DateTime.UtcNow,
                Venue = venue,
                Name = venueModel.Name,
                City = venueModel.City
            });
            await repository.SaveChangesAsync();
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