using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace post;

public class CommentService{
    
    private DatabaseContext context;
    public CommentService(DatabaseContext context){
         this.context = context;
    }

    public Comment AddComment(string title, string body, int userId, int postId){
        User? user = context.Users.Find(userId);
        Post? post = context.Posts.Find(postId);
        if(user == null){
            throw new ArgumentException("User not found");
        }
         if(post == null){
            throw new ArgumentException("Post not found");
        }
        Comment newComment = new Comment(title,body, user, post);
        context.Comments.Add(newComment);
        context.SaveChanges();
        return newComment;
    }

    public Comment RemoveComment(int id){
        Comment? comment = context.Comments.Find(id);
        if(comment == null){
            throw new ArgumentException("Comment not found whit this id");
        }
        context.Comments.Remove(comment);
        context.SaveChanges();

        return comment;
    }

      public Comment UpdateComment(int id, string title, string body){
        Comment? comment = context.Comments.Find(id);
        if(comment == null){
            throw new ArgumentException("Comment not found whit this id");
        }
        comment.Title = title;
        comment.Body = body;
        context.Comments.Update(comment);
        context.SaveChanges();

        return comment;
    }

}