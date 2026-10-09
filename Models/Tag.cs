//this file will hold the model for tags 
// copied and pasted from Comic.cs then edited
namespace ComicApi.Models;

public class Tag
{
    public int TagId {get; set; } 

    public string TagName {get; set; } = string.Empty;

    public DateOnly? CreatedAt {get; set;}
}