//Program.cs is like a javascript server.js
//it helps set up app, add middleware.
// define the routes and start the server.

//MySqlConnector gives access to MySqlConnection and 
//MySqlCommand. 
using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);

// variable for the connection string
var connectionString = builder.Configuration.GetConnectionString("Default");

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/test", async () =>
{ 
    using var connection = new MySqlConnection(connectionString);
    await connection.OpenAsync();

    using var cmd = new MySqlCommand("SELECT COUNT(*) FROM comics", connection);
    var result = await cmd.ExecuteScalarAsync();


    return $"Conncted! Comics: {result}";
    
});


app.Run();


