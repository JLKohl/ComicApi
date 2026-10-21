//Program.cs is like a javascript server.js
//it helps set up app, add middleware.
// define the routes and start the server.

//MySqlConnector gives access to MySqlConnection and 
//MySqlCommand. 
using MySqlConnector;
//lets Program.cs find the model classes
using ComicApi.Models;


var builder = WebApplication.CreateBuilder(args);
// variable for the connection string
var connectionString = builder.Configuration.GetConnectionString("Default");

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//adding CORS permission. The information 
//will not be able to print to the page if
//we do not add an AddPolicy 
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowComicSite", policy =>
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod());
    
});

var app = builder.Build();

//Using the CORS allower
app.UseCors("AllowComicSite");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

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
            // IsDBNull uses a position number and GetOrdinal
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

//this app.MapGet is getting all of the information individually
// and then giving the information together to the Client as a JSON
//so that we can have all of the information about the comics
// and what is attached to them
app.MapGet("/comics/{id}/details", async (int id) =>
{

    using var connection = new MySqlConnection(connectionString);
    await connection.OpenAsync();


    //this section is getting the comic
    using var comicCmd = new MySqlCommand(
        "SELECT comic_id, title, description, episode, created_at FROM comics WHERE comic_id = @id",
        connection
    );
    comicCmd.Parameters.AddWithValue("@id", id);

    //this part of the code is the 
    //package and holds the information
    // about the comics
    var details = new ComicDetails();

    //Using the block form of using so that 
    // the first reader will end and we can execute
    // more than one query inside of the MapGet. 
    //Connections can only have one reader open at a time.
    using (var reader = await comicCmd.ExecuteReaderAsync())
    {
        //if no comic was found we return
        // a not found
        if (!await reader.ReadAsync())
        {
            return Results.NotFound();
        } 

        var comic = new Comic
        {
            ComicId = reader.GetInt32("comic_id"),
            Title = reader.GetString("title"),
            Description = reader.IsDBNull(reader.GetOrdinal("description")) ? null : reader.GetString("description"),
            Episode = reader.IsDBNull(reader.GetOrdinal("episode")) ? null : reader.GetInt32("episode"),
            CreatedAt = reader.IsDBNull(reader.GetOrdinal("created_at")) ? null : reader.GetDateOnly("created_at")
        };

        //adding the new comic details into
        // the comic details variable 
        details.Comic = comic;
    }

    //this section is getting the characters
    using var charCmd = new MySqlCommand(
        """
        SELECT characters.character_id, name, age, gender, characters.description, characters.created_at 
        FROM characters 
        JOIN comic_characters ON comic_characters.character_id = characters.character_id
        WHERE comic_characters.comic_id = @id
        """,
        connection
    );
    charCmd.Parameters.AddWithValue("@id", id);

        using (var reader = await charCmd.ExecuteReaderAsync())
    {
        //making a list of characters that go with the comic
        var characters = new List<Character>();

        //using a loop because we are making
        // a list of characters in each comic
        while (await reader.ReadAsync())
        {
            characters.Add(new Character
            {
                CharacterId = reader.GetInt32("character_id"),
                Name = reader.GetString("name"),
                Age = reader.IsDBNull(reader.GetOrdinal("age")) ? null : reader.GetInt32("age"),
                Gender = reader.IsDBNull(reader.GetOrdinal("gender")) ? null : reader.GetString("gender"),
                Description = reader.IsDBNull(reader.GetOrdinal("description")) ? null : reader.GetString("description"),
                CreatedAt = reader.IsDBNull(reader.GetOrdinal("created_at")) ? null : reader.GetDateOnly("created_at")
            });
                
            
            //adding the new character details into
            // the comic details variable 
            details.Characters = characters; 
        } 

    }

        //this section is getting the tags
        using var tagCmd = new MySqlCommand(
        """
        SELECT tags.tag_id, tag_name, tags.created_at 
        FROM tags 
        JOIN comic_tags ON comic_tags.tag_id = tags.tag_id
        WHERE comic_tags.comic_id = @id
        """,
        connection
    );
        tagCmd.Parameters.AddWithValue("@id", id);

            using (var reader = await tagCmd.ExecuteReaderAsync())
        {
            //making a list of tags that go with the comic
            var tags = new List<Tag>();

            while (await reader.ReadAsync())
            {
                tags.Add(new Tag
                {
                    TagId = reader.GetInt32("tag_id"),
                    TagName = reader.GetString("tag_name"),
                    CreatedAt = reader.IsDBNull(reader.GetOrdinal("created_at")) ? null : reader.GetDateOnly("created_at")
                });
                    
                details.Tags = tags; 
            } 

        }

        
        return Results.Ok(details);

});


app.Run();

