using System.Text.Json.Serialization;

namespace post;

//Post har many to one för users 
//One to meny från comments 
public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public User User { get; set; }

    public Post() { }

    public Post(string title, string body, User user)
    {
        this.Title = title;
        this.Body = body;
        this.User = user;
    }
}

public class CreatePostDto
{
    public string Title { get; set; } = "";
    public string Body { get; set; } = "";
    // public int Id {get; set;} = 0;
}

public class PostDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }

    public PostDto(Post power)
    {
        this.Id = power.Id;
        this.Title = power.Title;
        this.Body = power.Body;
    }
}