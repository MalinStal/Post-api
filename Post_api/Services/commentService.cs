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

}