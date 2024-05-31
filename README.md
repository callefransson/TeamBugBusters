#TeamBugBusters 🐞💪

This is a .NET Core project that uses Entity Framework Core and Identity for user management.
Project Structure 🗂️

    Context: Contains the MyDbContext class which is used for interacting with the database. 🛠️
    Models: Contains the Product model class. 📦
    Areas/Identity/Pages/Account: Contains various classes for account management, such as password reset, email confirmation, two-factor authentication, etc. 🔐📧

Setup 🛠️

    Ensure you have .NET Core SDK installed on your machine. 💻
    Clone this repository. 📂
    Navigate to the project directory in your terminal. 🧭
    Run dotnet restore to restore the necessary NuGet packages. 📦
    Update the connection string in MyDbContext.cs to point to your SQL Server instance. 🔗
    Run dotnet ef migrations add InitialCreate to create a new migration. 🛠️
    Run dotnet ef database update to apply the migration and update the database. 📊
    Run dotnet run to start the application. ▶️

Features 🌟

    User registration and login with Identity. 👥
    Wheather statistics 🌥️

