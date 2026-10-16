//this file will hold the model for characters
namespace ComicApi.Models;
//The : is the "extends" for
//  using the abstract model in C# 
public class Character : BaseModel
{
    public int CharacterId {get; set; } 

    public string Name {get; set; } = string.Empty;

    public int? Age {get; set; }

    public string? Gender {get; set; }
    
    public string? Description {get; set;}

}