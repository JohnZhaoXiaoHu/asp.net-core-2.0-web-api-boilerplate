# An asp.net core 2.0 web api boilerplate using identity server 4  

### Current Status  
The project is being actively developed.  
Breaking changes and refactors still happens.  
An Angular 6 client was added, but only implemented one example which contains a paginated query to ProductController.

### Target
This project will implement the only must-need feature for a web api project.  

### Setup  
You need to run both AuthorizationServer Project and SalesApi.Web Project.  

For AuthorizationServer Project, it needs a certificate, it locates in Certificates folder, and the password for the certificate is in the launchSettings.json file.  

Both Projects need these environment variables, I set it up in launchSettings.json, you can set there in your system/user level.  
**Environment variables:**   
* **AuthorizationServer:ServerBase**, this is the authorization server base. such as http://localhost:5000.   
* **AuthorizationServer:SigningCredentialCertificatePath**, this is the authorization server certificate path. such as "E:/xxx.pfx".  
* **AuthorizationServer:SigningCredentialCertificatePassword**, this is the authorization server certificate password. such as "12345678"  
* **AuthorizationServer:DefaultConnection**, this is the mssql server connection string for authorization server. such as "Server=localhost; Database=AuthorizationServer; User Id=sa; Password=Bx@steel; MultipleActiveResultSets=true".  
* **SalesApi:ServerBase**, this is the sales api server base. such as http://localhost:5100.     
* **SalesApi:ClientBase**, this is the sales client uri base. such as http://localhost:4200.  
* **SalesApi:DefaultConnection**, this is the mssql server connection string. such as "Server=localhost; Database=SalesApi; User Id=sa; Password=Bx@steel; MultipleActiveResultSets=true".  

### Run  
You can run Authorization Server through Visual Studio 2017 or through dotnet cli using "dotnet run" command.  
You can run Sales.Api through Visual Studio 2017 or through dotnet cli using "dotnet watch run" or "dotnet run" command.  
You can run the ng6-client demo in these steps:  
1. run ```cd ng6-client && npm install``` to install dependecies of the project.
2. run ```npm install @angular-cli -g``` to install angular cli.
3. run```ng s -o ``` to run the client project.

**For Entity Framework core:**  
You can use migration commands both in Visual Studio 2017 or through "dotnet ef" command using dotnet cli.  

### Third party libraries:  
* AutoMapper
* Serilog (Write to Console, RollingFile, MSSql Server)
* Fluent Validation
* Swagger

## Features:
* Unit of work + Repository Pattern applied.  
* HATEOAS support.
* Paging, sorting and searching implemented in generic repository.
* Basic CRUD Best Practice applied in Controllers and Repository. 
* The VehicleController and CustomerController Implemented HATEOAS architecture style in different ways.  

![HATEOAS](https://raw.githubusercontent.com/solenovex/asp.net-core-2.0-web-api-boilerplate/master/Screenshots/1.png)

## Roadmap for May, 2018
* More Complex Entity Framework Core examples
* Refactoring using Design Patterns and Best Practice Principles.
* After Refactoring, Adding Some Unit Tests.
* Complete the Angular 6 client for these APIs.
* Add MongoDB support.
