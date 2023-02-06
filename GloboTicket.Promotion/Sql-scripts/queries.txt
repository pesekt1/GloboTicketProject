Delete from Venue;

INSERT INTO Venue (Name, City, VenueGuid)
VALUES ('test name', 'test city', NEWID());

INSERT INTO Venue (Name, City, VenueGuid)
VALUES ('test name2', 'test city2', NEWID());


select * from Venue;

select * 
from Venue v
left join VenueDescription d
on d.VenueId = v.VenueId;