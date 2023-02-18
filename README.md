# GloboTicket

## Branches - ordered:
- initial-state
- venue1
- venue2
- venue3
- venue4
- venue5
- acts-and-shows1
- indexer-and-emailer1
- commutative-messages1
- commutative-messages2

Present state of the database:

## System Architecture:
![Indexer Emailer2 V4](indexer_emailer2_v4.png)

## Dependencies - running containers
Access RabbitMQ: http://localhost:15672/

Access Elasticsearch: 
- http://localhost:9200
- http://localhost:9200/shows/_search

## Commutativity implemented
Commutativity of the message handlers is implemented. Even if the messages are processed out of order, the indexer will update the elasticsearch records correctly.


## Present state of the MSSQL database:


![E R D Venue Location Time Zone](ERD_venueLocation_TimeZone.png)
