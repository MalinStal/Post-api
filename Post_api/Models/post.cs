using System.Text.Json.Serialization;

namespace post;

//Post har many to one för users 
//One to meny från comments 
public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    [JsonIgnore]
    public User User { get; set; }
    public List<Comment> Comments {get; set;}

    public Post() { }

    public Post(string title, string body, User user, List<Comment> comment)
    {
        this.Title = title;
        this.Body = body;
        this.User = user;
        this.Comments = comment;
    }
}

public class CreatePostDto
{
    public string Title { get; set; } = "";
    public string Body { get; set; } = "";
    public int User {get; set;} = 0;
     public int Post {get; set;} = 0;
     public List<CreateCommentDto> Comments {get; set;} = new List<CreateCommentDto>();
}

public class PostDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }

    public List<CommentDto> Comments {get; set;}

    public PostDto(Post post)
    {
        this.Id = post.Id;
        this.Title = post.Title;
        this.Body = post.Body;
        this.Comments = post.Comments.Select(comment => new CommentDto(comment)).ToList();
    }
}