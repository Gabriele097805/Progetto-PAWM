# ToDo List

## Project configuration

### EFCore

To create the migration, it is necessary to install the EFCore CLI package using this command:

```powershell
dotnet tool install --global dotnet-ef
dotnet ef migrations add <name of the migration>
```

In the project, the package `Microsoft.EntityFrameworkCore.Design` must be installed.

To update the database with the new migrations, execute this command

```powershell
dotnet ef database create
```
To run the application it is necessary to have a GitHub application and change the values:
```
"ClientId": "",
"ClientSecret": "",
```
under GitHubConfiguration in the file "appsettings.json", or contact the developer for the ClientSecret.
