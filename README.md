# GloboTicket

## Added 2 microservices:
- Indexer
- Emailer

Both Indexer and Emailer are for now just writing a message on the console.

## System Architecture:

Fan out pattern - both Emailer and Indexer are getting the same messages from Promotion.
![Emailer Indexer1 V6](emailer_indexer1_v6.png)

## Present state of the MSSQL database:
![E R D Venue Location Time Zone](ERD_venueLocation_TimeZone.png)

