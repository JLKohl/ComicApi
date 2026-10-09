//this file will hold the model for characters
namespace ComicApi.Models;

public class Character
{
    public int CharacterId {get; set; } 

    public string Name {get; set; } = string.Empty;

    public int? Age {get; set; }

    public string? Gender {get; set; }
    
    public string? Description {get; set;}

    public DateOnly? CreatedAt {get; set;}
}