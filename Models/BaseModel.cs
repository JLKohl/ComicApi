namespace ComicApi.Models;

//this is an abstract class that the other three models
// will pull from to inherit their
// CreatedAt property
public abstract class BaseModel
{
    public DateOnly? CreatedAt {get; set;}
}