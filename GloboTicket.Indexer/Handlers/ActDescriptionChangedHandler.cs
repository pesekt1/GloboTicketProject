using GloboTicket.Indexer.Documents;
using GloboTicket.Promotion.Messages.Acts;
using System;
using System.Threading.Tasks;

namespace GloboTicket.Indexer.Handlers
{
    public class ActDescriptionChangedHandler
    {
        private readonly IRepository repository;

        public ActDescriptionChangedHandler(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task Handle(ActDescriptionChanged actDescriptionChanged)
        {
            Console.WriteLine($"Updating index for act {actDescriptionChanged.description.title}.");
            try
            {
                //throw new InvalidOperationException("Simulated failure");
                string actGuid = actDescriptionChanged.actGuid.ToString().ToLower();
                ActDescription actDescription = ActDescription.FromRepresentation(actDescriptionChanged.description);
                await repository.UpdateShowsWithActDescription(actGuid, actDescription);
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
