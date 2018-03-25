# An asp.net core 2.0 web api boilerplate using identity server 4
## Sales Api Structure:  
![Sales Api Structure](https://images2018.cnblogs.com/blog/986268/201803/986268-20180326061113994-1904339898.png)  

### Setup  
You need to run both AuthorizationServer Project and SalesApi.Web Project.  

For AuthorizationServer Project, it needs a certificate, it locates in Certificates folder, and the password for the certificate is in the launchSettings.json file.  

Both Projects need these environment variables, I set it up in launchSettings.json, you can set there in your system/user level.  
**Environment variables:**   
* **MLH:AuthorizationServer:ServerBase**, this is the authorization server base. such as http://localhost:5000.   
* **MLH:AuthorizationServer:SigningCredentialCertificatePath**, this is the authorization server certificate path. such as "E:/xxx.pfx".  
* **MLH:AuthorizationServer:SigningCredentialCertificatePassword**, this is the authorization server certificate password. such as "12345678"  
* **MLH:SalesApi:ServerBase**, this is the sales api server base. such as http://localhost:5100.     
* **MLH:SalesApi:ClientBase**, this is the sales client uri base. such as http://localhost:4200.  

### Run  
You can run Authorization Server through Visual Studio 2017 or through dotnet cli using "dotnet run" command.  
You can run SalesApi.Web through Visual Studio 2017 or through dotnet cli using "dotnet watch run" or "dotnet run" command.  

**For Entity Framework core:**  
You can use migration commands both in Visual Studio 2017 or through "dotnet ef" command using dotnet cli.  

### Third party libraries:  
* AutoMapper
* Serilog (Write to Console, RollingFile, MSSql Server)
* Fluent Validation
* Swagger
