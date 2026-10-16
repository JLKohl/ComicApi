namespace ComicApi.Models;

//this is an abstract that the other three models 
// will pull from this class to inherit there 
// CreatedAt property
public abstract class BaseModel
{
    public DateOnly? CreatedAt {get; set;}
}