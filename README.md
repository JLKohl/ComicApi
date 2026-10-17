# Overview

This project is a C# REST API built with ASP.NET Core. It works with a MySQL database to pull JSON to the browser so that you can view the information from the database. The API serves comics from my comic_site database along with their associated tags and characters.

I wrote this project code to help me learn more about using C# in a backend set up with MySQL database. Coming from a background of Node Express I am excited to expand my knowledge into other code that can be used in a full stack web development environment. The current stack flow I am working to learn is MySQL --> C# --> TypeScript. 

[Software Demo Video](https://youtu.be/74C6iu6MmOE?si=2ld2wEVIdYHzdTfI)

# Development Environment

## Tools used:
- [.NET SDK](https://dotnet.microsoft.com/en-us/) (10.0.101): the platform used to build and run the API. It provides the dotnet command.
- [MySQL Community Server](https://dev.mysql.com/downloads/) (26.7.0): database that stores comics, tags and characters.
- [VS Code](https://code.visualstudio.com/) (1.140.0): IDE used for writing code
- [REST Client](https://marketplace.visualstudio.com/items?itemName=humao.rest-client) (0.25.1): VS Code extension by Huachao Mao, used with `ComicApi.http` to test the endpoints
- [Claude Code](https://claude.com/download) (Claude Opus 5.5): AI assistant used in VS Code for explaining C# and ASP.NET Core concepts and reviewing code
- MySQL Workbench: optional, used to view and query database during development. 

## Language and Libraries:
- [C#](https://learn.microsoft.com/dotnet/csharp) (14): Language used to write API.
- [ASP.NET Core](https://learn.microsoft.com/aspnet/core) (10): Web framework, this routes `MapGet`, requests and JSON responses.
- [MySqlConnector](https://mysqlconnector.net) (2.6.2): Library that connects to MySQL and runs SQL. 
- [Microsoft.AspNetCore.OpenApi](https://www.nuget.org/packages/Microsoft.AspNetCore.OpenApi/10.0.1) (10.0.1): Generates a description of API's endpoints in development.
- [Microsoft.OpenApi](https://www.nuget.org/packages/Microsoft.OpenApi) (2.7.5): supporting package for Microsoft.AspNetCore.OpenApi.


*Packages are installed automatically when you run `dotnet run`.*
 
# Getting Started

## Prerequisites 
 - Make sure you have .NET SDK and MySQL Community Server installed on your computer. Details listed under [Tools Used](#tools-used).

## Get the Code
- Run the following code in your IDE terminal
```bash
git clone https://github.com/JLKohl/ComicApi.git
cd ComicApi
```
## Create the database
- The comic_site database can be recreated by using the database/comic_site.sql. 
- In your terminal run 
```bash
mysql -u <your_username> -p < database/comic_site.sql
```
- Replace `<your_username>` with your MySQL username.
- If `mysql` isn't found on a Mac, try `/usr/local/mysql/bin/mysql`

## Set the connection string
- Set up a user connection string, which is stored in user secrets, so that you can connect to your MySQL.
- Run from ComicApi folder
```bash
dotnet user-secrets set "ConnectionStrings:Default" 'Server=localhost;Port=3306;Database=comic_site;User=<your_username>;Password=<your_password>'
```
- Replace `<your_username>` with your MySQL username.
- Replace `<your_password>` with your MySQL password.

## Run the API
- Use the command `dotnet run` to start the API
- URL: http://localhost:5147

## Try it out
- `GET /comics`: returns all comics
- `GET /comics/{id}`: returns one comic by its ID
- `GET /comics/{id}/details`: returns one comic with its associated tags and characters
- `ComicApi.http` includes ready-made requests if you use VS Code with a REST client. The one I used is listed under [Tools Used](#tools-used). 

# Useful Websites

- [GeeksforGeeks](https://www.geeksforgeeks.org/linux-unix/install-and-setup-dotnet-sdk-on-windows-macos-and-linux/#google_vignette)
- [w3Schools](https://www.w3schools.com/sql/sql_join_inner.asp)

# Future Work

- Add more comic CRUD endpoints such as POST, PUT and DELETE for creating new comics 
- Add more endpoints for characters and tags (such as GET, PUT, POST, DELETE) so they could be more usable in an admin panel
- Separate the `MapGet` calls into other files so that `Program.cs` is not so crowded
