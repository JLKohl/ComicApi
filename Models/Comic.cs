//this file will hold the model for comics
// which will be filled with the data from 
//the database by using a blueprint of what is in
// the database. The endpoint code will be in Program.cs 
//which will read the rows from the database and fill 
// the information in.
namespace ComicApi.Models;

public class Comic
{
    public int ComicId {get; set; } 

    //have to set the string to something before it runs
    // because it can not be null. The string.Empty gives
    // it an empty string to hold onto so that it is not 
    // null
    public string Title {get; set; } = string.Empty;
    
    //adding the ? means that the column can be null
    public string? Description {get; set;}

    public int? Episode {get; set;}

    public DateOnly? CreatedAt {get; set;}
}