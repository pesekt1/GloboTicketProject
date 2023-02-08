# GloboTicket

## Migrating the database into immutable form:
Original state:
Venue table

### 1.Step: Add immutable records:
- Generate AddImmutableRecords migration based on the entity classes we added VenueDescription and VenueRemoved.
- Update the local database.
![Adding Immutable Records](addingImmutableRecords.png)

### 2.Step: Creating a migration script

Create MigrateVenueToImmutableRecords migration - we need to write the scripts for the Up and Down methods:

```C#
    public partial class MigrateVenueToImmutableRecords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO VenueDescription
	                (VenueId, Name, City, ModifiedDate)
                SELECT VenueId, Name, City, GETUTCDATE()
                FROM Venue");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"UPDATE v
                SET
	                Name = d.Name,
	                City = d.City
                FROM Venue v
                JOIN (
	                SELECT VenueId, Name, City,
		                ROW_NUMBER() OVER (
			                PARTITION BY VenueId
			                ORDER BY ModifiedDate DESC
		                ) AS row
	                FROM VenueDescription
                ) as d
                    ON v.VenueId = d.VenueId
                    AND d.row = 1
                ");
        }
    }
}
```

- Up method: Insert existing data from Venue into VenueDescription.
- Down method: We need to find the last description records and set Name and City properties in Venue table. There might be multiple rows in VenueDescription corresponding to a single venue (if it was updated) - we need the most recent record. 

### 3.Step: Remove the state from Venue table:
- Remove City and Name properties from Venue entity class.
- Generate a migration for this
- Apply the migration to your local databse.

![Immutable](immutable.png)

Now our database is in immutable form and we migrated the existing data as well.