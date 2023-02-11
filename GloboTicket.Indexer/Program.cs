using GloboTicket.Promotion.Messages.Shows;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace GloboTicket.Indexer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(busConfig =>
            {
                busConfig.Host("rabbitmq://localhost");
                busConfig.ReceiveEndpoint("GloboTicket.Indexer", endpointConfig =>
                {
                    endpointConfig.Handler<ShowAdded>(async context =>
                        await new ShowAddedHandler().Handle(context.Message));
                });
            });

            await bus.StartAsync();

            Console.WriteLine("Receiving messages. Press a key to stop.");
            await Task.Run(() => Console.ReadKey());

            await bus.StopAsync();
        }
    }
}
