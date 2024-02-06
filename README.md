#GloboTicket demo

ASP.NET Solution

Dependencies needed:
- runtime for dotnet 5
- SDK dotnet 5

(Tested with Visual Studio 2019)


Initial state

Using MSSQL:

Microsoft SQL Server docker image:
https://learn.microsoft.com/en-us/sql/linux/quickstart-install-connect-docker?view=sql-server-ver16&pivots=cs1-bash

docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Pass@word1" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest

Connection string for MSSQL docker container: appsettings.json
  "ConnectionStrings": {
    "PromotionContext": "Server=(localdb)\\mssqllocaldb;Database=PromotionContext-3a251c60-adcc-4730-9e6f-c8ec872f7f87;Trusted_Connection=True;MultipleActiveResultSets=true"
  }


Connect to the docker mssql database server in Visual Studio:
Server: (localdb)\mssqllocaldb
Database name: ...


For localhost MSSQL server:
the server name is: <computer_name>\SQLEXPRESS - for example DESKTOP-E8OC23F\SQLEXPRESS
connection string will start with: Server=localhost\\SQLEXPRESS;