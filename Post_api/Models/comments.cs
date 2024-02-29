using System.Text.Json.Serialization;
using Npgsql.Replication;

namespace post;

//Coments har meny to one relation med post och user
public class Comment{
    public int Id {get; set;}
    public string Title {get; set;}
    public string Body {get; set;}
    [JsonIgnore]
    public User User {get; set;}
    [JsonIgnore]
    public Post Post {get; set;}

   public Comment(){}

    public Comment(string title,string body, User user, Post post){
        Title = title;
        Body = body;
        User = user;
        Post = post;
        
    }
}

public class CreateCommentDto
{
  public string Title {get; set;} = "";
  public string Body {get; set;}   = "";
  public int User { get; set;} =0;
  public int Post { get; set;} =0;

  
}

public class CommentDto{
  public int Id {get; set;}
 public string Title {get; set;}
  public string Body {get; set;}  

  public int User {get; set;}


//public CommentDto(){}
  public CommentDto( Comment comment){
    this.Id = comment.Id;
     this.Title = comment.Title;
      this.Body = comment.Body;
     
   
  }
}

