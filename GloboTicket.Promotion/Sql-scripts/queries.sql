Use [PromotionContext-v3];

Delete from Venue;

INSERT INTO Venue (Name, City, VenueGuid)
VALUES ('Madison Square Garden', 'New York', NEWID());

INSERT INTO Venue (Name, City, VenueGuid)
VALUES ('White Eagle Hall', 'Jersey City', NEWID());


select * from Venue;


begin transaction

SELECT VenueId, GETUTCDATE() , Name, City
FROM Venue

select * 
from Venue v
left join VenueDescription d
on d.VenueId = v.VenueId;

rollback transaction



begin transaction

INSERT INTO VenueDescription(VenueId, Name, City, ModifiedDate)
                SELECT VenueId, Name, City, GETUTCDATE()
                FROM Venue

UPDATE Venue
SET
	Name = NULL,
	City = NULL


select * 
from Venue v
left join VenueDescription d
on d.VenueId = v.VenueId;

rollback transaction


SELECT VenueId, Name, City,
	ROW_NUMBER() OVER (
		PARTITION BY VenueId
		ORDER BY ModifiedDate DESC
	) AS row
FROM VenueDescription

insert into VenueDescription(VenueId, Name, City, ModifiedDate)
values (8, 'Old Madison Square Garden', 'New York', DATEADD(DD, -2, GETUTCDATE()));

SELECT *
from Venue v
join(
	SELECT VenueId, Name, City,
		ROW_NUMBER() OVER (
			PARTITION BY VenueId
			ORDER BY ModifiedDate DESC
		) AS row
	FROM VenueDescription
) as d
ON v.VenueId = d.VenueId 
AND d.row = 1; -- only the first row - most recent update


update v
set 
	Name = d.Name,
	City = d.City
from Venue v
join(
	SELECT VenueId, Name, City,
		ROW_NUMBER() OVER (
			PARTITION BY VenueId
			ORDER BY ModifiedDate DESC
		) AS row
	FROM VenueDescription
) as d
ON v.VenueId = d.VenueId 
AND d.row = 1; -- only the first row - most recent update