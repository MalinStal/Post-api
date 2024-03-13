using System.Text.Json.Serialization;

namespace post;

//Post har many to one för users 
//One to meny från comments 
public class Post
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Body { get; set; }
    [JsonIgnore]
    public User? User { get; set; }
    public List<Comment>? Comments {get; set;}
    public List<FileModel>? Images {get; set;}

   

    public Post() { }

    public Post(string title, string body, User user, List<Comment> comment, List<FileModel> files)
    {
        this.Title = title;
        this.Body = body;
        this.User = user;
        this.Comments = comment;
        this.Images = files;
  

    }
    
    }