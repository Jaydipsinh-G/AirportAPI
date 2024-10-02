# Airport System

## Summary
The Airport System is a minimal API built using .NET 8 and C#. It provides functionality for fetching, creating, and deleting data related to airports, countries, and routes. This API can be used to manage airport information and perform operations on the data.

## User Guide

### Connection String Setup
To set up the connection string for the Airport System, follow these steps:
1. Open the `appsettings.json` file in the project.
2. Locate the `ConnectionStrings` section.
3. Replace the value of the `DefaultConnection` key with your desired connection string.

### Database Creation
To create the database for the Airport System, follow these steps:
1. Open the Package Manager Console in Visual Studio.
2. Set the default project to the project containing the Airport System.
3. Run the following command: `dotnet ef database update`.

### Setup Project
To set up the Airport System project, follow these steps:
1. Clone the repository to your local machine.
2. Open the solution file in Visual Studio.
3. Build the solution to restore NuGet packages.
4. Set up the connection string as described in the "Connection String Setup" section.
5. Create the database as described in the "Database Creation" section.
6. Run the project and start using the Airport System API.
