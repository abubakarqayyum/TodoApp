************************************************************ Project Setup ************************************************************

1): Make sure you have .net8 installed on your machine.

2): Change the database server name and its username & password.

3): If you want to run this project on https then commnet out this line ["applicationUrl": "http://localhost:5124"] and uncomment this line ["applicationUrl": "https://localhost:7040;http://localhost:5124",]
    In launchSettings.json along with this uncomment this [app.UseHttpsRedirection()] from program.cs.

4): API collection is available in the docs folder in API project.

5): Run this project. (Use postman collection to perform operations)




********************************************************** Project Structure **********************************************************

1): API project is the entry point of the application along with this it is responsible for model validation, middleware ,and other project configurations

2): BusinessLogic handling all the business logic and mapping responses into DTO's using automapper.

3): DataAccess is handling database operations.

4): Enities contains database entity structure and there mapping with EF Core.

5): Utilities provide support operations at one place that can used any where in the application.
