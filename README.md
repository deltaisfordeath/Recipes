# Under Construction!

## Recipe Management Web App

This is a pet project of mine to grow and solidify my knowledge of building web applications using .NET Core. 

Configured to use a MySQL database. 

### To run 
- rename `appsettings.json.sample` to `appsettings.json`.
- Add your connection string to `Default` in `appsettings.json`.
- Run `dotnet ef migrations add initialize_db`.
- Run `dotnet ef database update`.
- Run `dotnet watch` or run from debugger of your chosen IDE.