using GloboTicket.Indexer.Documents;
using System.Threading.Tasks;

namespace GloboTicket.Indexer
{
    public interface IRepository
    {
        Task<VenueDocument> GetVenue(string venueGuid);
        Task<ActDocument> GetAct(string actGuid);

        Task IndexVenue(VenueDocument venue);
        Task IndexAct(ActDocument act);
        Task IndexShow(ShowDocument show);

        Task UpdateShowsWithVenueDescription(string venueGuid, VenueDescription venueDescription);
        Task UpdateShowsWithVenueLocation(string venueGuid, VenueLocation venueLocation);
        Task UpdateShowsWithActDescription(string actGuid, ActDescription actDescription);
    }
}