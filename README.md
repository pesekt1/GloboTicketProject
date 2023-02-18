# GloboTicket

## System Architecture:
![Indexer Emailer2 V4](indexer_emailer2_v4.png)

Indexer is handling 4 different message types that are published to the Indexer queue.

## Dependencies - running containers
Access RabbitMQ: http://localhost:15672/

Access Elasticsearch: 
- http://localhost:9200
- http://localhost:9200/shows/_search

## Commutativity problem
Commutativity of the message handlers is not implemented. We can get problems if the messages are handled out of order.

Simulate this problem by throwing an exception inside the handler:
```C#
        public async Task Handle(ActDescriptionChanged actDescriptionChanged)
        {
            Console.WriteLine($"Updating index for act {actDescriptionChanged.description.title}.");
            try
            {
                throw new InvalidOperationException("Simulated failure");
```
The message will go into the Error queue. Now we can remove the exception and create a new message which will be processed by the indexer.
Now we can move the first message from the Error queue into the Indexer queue and it will be processed out of order... It will update the elasticsearch record so the last update will be lost.

## Present state of the MSSQL database:


![E R D Venue Location Time Zone](ERD_venueLocation_TimeZone.png)

