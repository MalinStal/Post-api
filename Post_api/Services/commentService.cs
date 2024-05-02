using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace post;

public class CommentService
{
    private DatabaseContext context;

    public CommentService(DatabaseContext context)
    {
        this.context = context;
    }

    public Comment AddComment(string title, string body, string userId, int postId)
    {
        User? user = context.Users.Find(userId);
        Post? post = context.Posts.Find(postId);
        if (user == null)
        {
            throw new ArgumentException("User not found");
        }
        if (post == null)
        {
            throw new ArgumentException("Post not found");
        }
        Comment newComment = new Comment(title, body, user, post);
        context.Comments.Add(newComment);
        context.SaveChanges();
        return newComment;
    }

    // public Comment RemoveComment(int id, string userId)
    // {
    //     User? user = context.Users.Find(userId);
    //     Comment? comment = context.Comments.Find(id);
    //     if (comment == null)
    //     {
    //         throw new ArgumentException("Comment not found whit this id");
    //     }
    //     if (user != null)
    //     {
    //         List<Comment>? comments = context
    //             .Comments.Where(comment => comment.User.Id == user.Id && comment.Id == id)
    //             .ToList();
    //     }
    //     context.Comments.Remove(comment);
    //     context.SaveChanges();

    //     return comment;
    // }
    public Comment RemoveComment(int id, string userId)
{
    User user = context.Users.Find(userId);
    if (user == null)
    {
        throw new ArgumentException("User not found");
    }

    Comment comment = context.Comments.FirstOrDefault(c => c.Id == id && c.User == user);
    if (comment == null)
    {
        throw new ArgumentException("Comment not found");
    }

    context.Comments.Remove(comment);
    context.SaveChanges();

    return comment;
}


    public Comment UpdateComment(string userId, int commentId, string title, string body)
    {
        User? user = context.Users.Find(userId);
        Comment? comment = context.Comments.Find(commentId);
        if (comment == null)
        {
            throw new ArgumentException("Comment not found whit this id");
        }
        List<Comment> findComment = context.Comments.Where(comment => comment.User.Id == user.Id && comment.Id == commentId ).ToList();
        if(findComment != null) {
            comment.Title = title;
        comment.Body = body;
        context.Comments.Update(comment);
        context.SaveChanges();

        return comment;
        }
  
        throw new ArgumentException("your not aloud to change this comment");
 
        
    }
}
