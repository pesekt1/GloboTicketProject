#GloboTicket

implementing client-side ID to achieve idempotent POST request.

add guid to Venue

override OnModelCreating

create a migration
update the database

remove default value generation from onModelCreating

create a migration 
update the database

Change the VenuesController actions to use guid

Test that the POST request is now idempotent:

Now we can test that if we click on Create button multiple times, we only get one record.
If we click on the Create button and then edit something and click on Create again it will update the record that was just created - it will not create a new record.
