namespace post;

public class Post
{
    public int id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }

    public int User_Id { get; set; }

    public List<Comment> AllComments;

    public Post() { }

    public Post(string title, string description, int user_id)
    {
        Title = title;
        Body = description;
        User_Id = user_id;
        AllComments = new List<Comment>();
    }
}

public class CreatePostDto
{
    public string Title { get; set; }
    public string Body { get; set; }

    public int User_id { get; set; }

    public CreatePostDto() { }

    public CreatePostDto(string title, string body, int id)
    {
        this.Title = title;
        this.Body = body;
        this.User_id = id;
    }
}
