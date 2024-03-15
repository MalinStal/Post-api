namespace post;

public class CreatePostDto
{
    public string Title { get; set; } = "";
    public string Body { get; set; } = "";
    public int User {get; set;} = 0;
     public int Post {get; set;} = 0;
     public List<CreateCommentDto> Comments {get; set;} = new List<CreateCommentDto>();
    public List<FileModelDto> Files {get; set;} = new List<FileModelDto>();
}

public class PostDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Body { get; set; }

    public List<CommentDto>? Comments {get; set;}
    public List<FileModelDto>? Files {get; set;}

    public PostDto(Post post)
    {
        this.Id = post.Id;
        this.Title = post.Title;
        this.Body = post.Body;
        this.Comments = post.Comments?.Select(comment => new CommentDto(comment)).ToList();
        this.Files = post.Images?.Select(file => new FileModelDto(file)).ToList();
    }
}