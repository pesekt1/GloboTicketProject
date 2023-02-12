using GloboTicket.Indexer.Documents;
using GloboTicket.Promotion.Messages.Venues;
using System;
using System.Threading.Tasks;

namespace GloboTicket.Indexer.Handlers
{
    public class VenueDescriptionChangedHandler
    {
        private readonly IRepository repository;

        public VenueDescriptionChangedHandler(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task Handle(VenueDescriptionChanged venueDescriptionChanged)
        {
            Console.WriteLine($"Updating index for venue {venueDescriptionChanged.description.name}.");
            try
            {
                string venueGuid = venueDescriptionChanged.venueGuid.ToString().ToLower();
                VenueDescription venueDescription = VenueDescription.FromRepresentation(venueDescriptionChanged.description);

                await repository.UpdateShowsWithVenueDescription(venueGuid, venueDescription);
                Console.WriteLine("Succeeded");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
