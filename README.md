*********************************************************** Introduction **************************************************************

A clean, modular, and production-ready **.NET 8 Web API** for managing Todos with **JWT Authentication**, **Fluent Validation**, and a **Generic Repository–Service Pattern** using **Entity Framework Core**.

********************************************************** Prerequisites **************************************************************

Make sure you have the following installed on your machine:

1): [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
2): SQL Server (LocalDB or any instance)
3): [Postman](https://www.postman.com/downloads/) for API testing


************************************************************ Project Setup ************************************************************

1): Change the database server name and its username & password.

2): If you want to run this project on https then commnet out this line ["applicationUrl": "http://localhost:5124"] and uncomment this line ["applicationUrl": "https://localhost:7040;http://localhost:5124",]
    In launchSettings.json along with this uncomment this [app.UseHttpsRedirection()] from program.cs.

3): API collection is available in the docs folder in API project.

4): Run this project. (Use postman collection to perform operations. BaseUrl postman for http [http://localhost:5124] and for https [https://localhost:7040])




********************************************************** Project Structure **********************************************************

1): Todo.API => Entry point of the application. Handles API endpoints, middleware, dependency injection, and validation.

2): Todo.BusinessLogic => Contains business logic, DTOs, AutoMapper profiles, and service layer implementations.

3): Todo.DataAccess =>  Implements repository pattern and database access using EF Core.

4): Todo.Entities => Contains entity models and their relationships.

5): Todo.Utilities => Common utility classes, constants, and helper methods used across projects.
