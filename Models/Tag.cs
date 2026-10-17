//this file will hold the model for tags
namespace ComicApi.Models;

public class Tag : BaseModel
{
    public int TagId {get; set; } 

    public string TagName {get; set; } = string.Empty;

}