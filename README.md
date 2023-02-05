#GloboTicket

implement cqrs in the code

separate commands and queries - create separate files

in program - register the queries and commands services

PromotionContext - add GetOrInsertVenue method - because it will be used multiple times

implement immutability of venues table:
create venueDescription and venueRemoved classes, and VenueInfo class
In venue class add collections for venueDescription and venueRemoved

Add unit testing project and run the tests - now some will fail and we need to fix them... TDD - test-driven development
