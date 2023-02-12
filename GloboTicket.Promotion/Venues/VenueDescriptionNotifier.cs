﻿using GloboTicket.Promotion.Data;
using GloboTicket.Promotion.Messages.Venues;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace GloboTicket.Promotion.Venues
{
    class VenueDescriptionNotifier : INotifier<VenueDescription>
    {
        private readonly IPublishEndpoint publishEndpoint;

        public VenueDescriptionNotifier(IPublishEndpoint publishEndpoint)
        {
            this.publishEndpoint = publishEndpoint;
        }

        public async Task Notify(VenueDescription venueDescription)
        {
            var venueDescriptionChanged = new VenueDescriptionChanged
            {
                venueGuid = venueDescription.Venue.VenueGuid,
                description = new VenueDescriptionRepresentation
                {
                    name = venueDescription.Name,
                    city = venueDescription.City,
                    modifiedDate = venueDescription.ModifiedDate
                }
            };

            await publishEndpoint.Publish(venueDescriptionChanged);
        }
    }
}