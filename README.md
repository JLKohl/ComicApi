# Overview

This project is a C# REST API built with ASP.NET Core. It works with a MySQL database to pull JSON to the browser so that you can view the information from the database. The API serves comics from my comic_site database along with their associated tags and characters.

I wrote this project code to help me learn more about using C# in a backend set up with MySQL database. Coming from a background of Node Express I am excited to expand my knowledge into other code that can be used in a full stack web development environment. The current stack flow I am working to learn is MySQL --> C# --> TypeScript. 

[Software Demo Video](http://youtube.link.goes.here)

# Development Environment

## Tools used:
- [.NET SDK](https://dotnet.microsoft.com/en-us/)(10.0.101, the platform used to build and run the API. it provides the dotnet command)
- [MySQL Workbench](https://dev.mysql.com/downloads/)()
- [MySQL Community Server]()()
- [VS Code](https://code.visualstudio.com/)(1.140.0, IDE used for writing code)
- [REST Client](https://marketplace.visualstudio.com/items?itemName=humao.rest-client) (VS Code extension by Huachao Mao,0.25.1), used with `ComicApi.http` to test the endpoints
- [Claude Code](https://claude.com/download)(Claude Opus 5.5, AI assistant used in VS Code for explaining C# and ASP.NET Core concepts and reviewing code)

## Language and Libraries:

# Getting Started


# Useful Websites

- [GeeksforGeeks](https://www.geeksforgeeks.org/linux-unix/install-and-setup-dotnet-sdk-on-windows-macos-and-linux/#google_vignette)
- [w3Schools](https://www.w3schools.com/sql/sql_join_inner.asp)

# Future Work

- Add more comic CRUD endpoints such as POST, PUT and DELETE for creating new comics 
- Add more endpoints for characters and tags (such as GET, PUT, POST, DELETE) so they could be more usable in an admin panel
- Separate the `MapGet` calls into other files so that `Program.cs` is not so crowded
