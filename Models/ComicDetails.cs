namespace ComicApi.Models;

public class ComicDetails
{
    //creating a new Comic object called Comic
    // new is a C# keyword. When you use it
    // C# already knows the type for that line
    // new() as a short cut instead of calling 
    // new Comic(). The () calls the constructor 
    public Comic Comic { get; set; } = new();

    //creating a list of Tag objects 
    public List<Tag> Tags { get; set; } = new();

    //creating a list of Character objects
    public List<Character> Characters { get; set; } = new();

}