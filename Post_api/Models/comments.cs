namespace post;
public class Comment{
    public int id {get; set;}
    public string Title {get; set;}
    public string Body {get; set;}
    public int User_id {get; set;}
    public int Post_id {get; set;}

   public Comment(){}

    public Comment(string title,string body, int userId, int postId){
        Title = title;
        Body = body;
        User_id =userId;
        Post_id = postId;
        
    }
}

public class CreateCommentDto
{
  public string Title {get; set;}
  public string Body {get; set;}  
  public int User_id { get; set;} 
  public int Post_id { get; set;} 

   public CreateCommentDto(){}
    public CreateCommentDto(
      string title, string body, int userid, int postid){
        Title = title;
        Body = body;
        User_id = userid;
        Post_id = postid;
      }
}