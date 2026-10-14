//Program.cs is like a javascript server.js
//it helps set up app, add middleware.
// define the routes and start the server.

//MySqlConnector gives access to MySqlConnection and 
//MySqlCommand. 
using MySqlConnector;
//lets Program.cs find the comic class
using ComicApi.Models;


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

// app.MapGet("/test", async () =>
// { 
//     using var connection = new MySqlConnection(connectionString);
//     await connection.OpenAsync();

//     using var cmd = new MySqlCommand("SELECT COUNT(*) FROM comics", connection);
//     var result = await cmd.ExecuteScalarAsync();


//     return $"Conncted! Comics: {result}";

// });

app.MapGet("/comics", async () =>
{
    using var connection = new MySqlConnection(connectionString);
    await connection.OpenAsync();

    using var cmd = new MySqlCommand("SELECT comic_id, title, description, episode, created_at FROM comics", connection);
    using var reader = await cmd.ExecuteReaderAsync();

    var comics = new List<Comic>();

    while (await reader.ReadAsync())
    {
        comics.Add(new Comic
        {
            //these get the information that is 
            // NOT NULLABLE so they just need reader.Get
            ComicId = reader.GetInt32("comic_id"),
            Title = reader.GetString("title"),
            //had to use IsDBNull on columns that can be null. 
            // IsDBnull uses a a position number and GetOrdinal 
            // gives us that number to use 
            Description = reader.IsDBNull(reader.GetOrdinal("description")) ? null : reader.GetString("description"),
            Episode = reader.IsDBNull(reader.GetOrdinal("episode")) ? null : reader.GetInt32("episode"),
            CreatedAt = reader.IsDBNull(reader.GetOrdinal("created_at")) ? null : reader.GetDateOnly("created_at")
        });
    }

    return comics;

});

app.MapGet("/comics/{id}", async (int id) =>
{
    using var connection = new MySqlConnection(connectionString);
    await connection.OpenAsync();

    using var cmd = new MySqlCommand(
        "SELECT comic_id, title, description, episode, created_at FROM comics WHERE comic_id = @id",
        connection
    );
    cmd.Parameters.AddWithValue("@id", id);

    //This variable holds the result of calling
    // ExecuteReaderAsync() on the cmd variable
    // The method will return a reader, which is 
    // a pointer that can move through the table 
    // one row at a time. The SQL code is also 
    // executed at this point because we used the method 
    // on cmd which contains the command we needed
    using var reader = await cmd.ExecuteReaderAsync();

    //ReadAsync() moves to the next row of the 
    // query's results. It returns true if there's 
    // a row, and false if the results are empty.
    if (await reader.ReadAsync())
    {
        var comic = new Comic
        {

            ComicId = reader.GetInt32("comic_id"),
            Title = reader.GetString("title"),
            Description = reader.IsDBNull(reader.GetOrdinal("description")) ? null : reader.GetString("description"),
            Episode = reader.IsDBNull(reader.GetOrdinal("episode")) ? null : reader.GetInt32("episode"),
            CreatedAt = reader.IsDBNull(reader.GetOrdinal("created_at")) ? null : reader.GetDateOnly("created_at")
        };

        return Results.Ok(comic);
    }

    return Results.NotFound();

});

//this app.Map get is getting all of the informations individally
// and then giving the information togeather to the Client as a JSON 
//so that we can have all of the infromation about the comics 
// and what is attached to them
app.MapGet("/comics/{id}/details", async (int id) =>
{

    using var connection = new MySqlConnection(connectionString);
    await connection.OpenAsync();

    using var cmd = new MySqlCommand(
        "SELECT comic_id, title, description, episode, created_at FROM comics WHERE comic_id = @id",
        connection
    );
    cmd.Parameters.AddWithValue("@id", id);

    //this part od the code is the 
    //package and holds the information
    // about the comics
    var details = new ComicDetails();

    return details;
});


app.Run();


