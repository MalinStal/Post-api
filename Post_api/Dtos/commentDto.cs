namespace post;

public class CreateCommentDto
{
    public string Title { get; set; } = "";
    public string Body { get; set; } = "";
    public int User { get; set; } = 0;
    public int Post { get; set; } = 0;
}

public class CommentDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Body { get; set; }

    public int User { get; set; }

    public CommentDto(Comment comment)
    {
        this.Id = comment.Id;
        this.Title = comment.Title;
        this.Body = comment.Body;
    }
}
