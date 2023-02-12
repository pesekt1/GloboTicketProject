﻿using GloboTicket.Indexer.Documents;
using GloboTicket.Indexer.Elasticsearch;
using GloboTicket.Indexer.Handlers;
using GloboTicket.Indexer.Updaters;
using GloboTicket.Promotion.Messages.Acts;
using GloboTicket.Promotion.Messages.Shows;
using GloboTicket.Promotion.Messages.Venues;
using GreenPipes;
using MassTransit;
using Nest;
using System;
using System.Threading.Tasks;

namespace GloboTicket.Indexer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
                .DefaultMappingFor<ShowDocument>(m => m
                    .IndexName("shows")
                )
                .DefaultMappingFor<ActDocument>(m => m
                    .IndexName("acts")
                )
                .DefaultMappingFor<VenueDocument>(m => m
                    .IndexName("venues")
                );
            var elasticClient = new ElasticClient(settings);
            var elasticsearchRepository = new ElasticsearchRepository(elasticClient);
            var actUpdater = new ActUpdater(elasticsearchRepository);
            var venueUpdater = new VenueUpdater(elasticsearchRepository);
            var showAddedHandler = new ShowAddedHandler(elasticsearchRepository, actUpdater, venueUpdater);
            var actDescriptionChangedHandler = new ActDescriptionChangedHandler(elasticsearchRepository, actUpdater);
            var venueDescriptionChangedHandler = new VenueDescriptionChangedHandler(elasticsearchRepository, venueUpdater);
            var venueLocationChangedHandler = new VenueLocationChangedHandler(elasticsearchRepository, venueUpdater);

            var bus = Bus.Factory.CreateUsingRabbitMq(busConfig =>
            {
                busConfig.Host("rabbitmq://localhost");
                busConfig.ReceiveEndpoint("GloboTicket.Indexer", endpointConfig =>
                {
                    endpointConfig.UseMessageRetry(r => r.Exponential(
                        retryLimit: 5,
                        minInterval: TimeSpan.FromSeconds(5),
                        maxInterval: TimeSpan.FromSeconds(30),
                        intervalDelta: TimeSpan.FromSeconds(5)));

                    endpointConfig.Handler<ShowAdded>(async context =>
                        await showAddedHandler.Handle(context.Message));
                    endpointConfig.Handler<ActDescriptionChanged>(async context =>
                        await actDescriptionChangedHandler.Handle(context.Message));
                    endpointConfig.Handler<VenueDescriptionChanged>(async context =>
                        await venueDescriptionChangedHandler.Handle(context.Message));
                    endpointConfig.Handler<VenueLocationChanged>(async context =>
                        await venueLocationChangedHandler.Handle(context.Message));
                });
            });

            await bus.StartAsync();

            Console.WriteLine("Receiving messages. Press a key to stop.");
            await Task.Run(() => Console.ReadKey());

            await bus.StopAsync();
        }
    }
}
