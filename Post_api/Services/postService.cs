using System.IO.Compression;
using Microsoft.EntityFrameworkCore;

namespace post;

public class PostService{
    
    private DatabaseContext context;
    public PostService(DatabaseContext context){
         this.context = context;
    }
 
     public Post CreatePost(string title, string body, string userId){
      if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("title must not be null or empty");
        }

        if (string.IsNullOrWhiteSpace(body))
        {
            throw new ArgumentException("content must not be null or empty");
        }
       User? user =  context.Users.Find(userId);
        if(user != null){

            Post newPost = new Post(title,body,user, new List<Comment>()); 
            context.Posts.Add(newPost);
            
           
            context.SaveChanges();
            return newPost;
        }
      throw new ArgumentException("there is no User connected to this id");
       
    }

    public Post DeletePost(int id, string userId){
       
       User? user = context.Users.Find(userId);
       if(user != null) {
            List<Post>? posts = context.Posts.Where(post => post.User.Id == user.Id && post.Id == id).ToList();
             
            if (posts != null){
                Post post = posts[0];
                context.Posts.Remove(post);
                context.SaveChanges();
                return post;
        
             }
              throw new ArgumentException("cant find post");
       } 
            throw new ArgumentException("cant find post");
         
       
    }

    public Post UpdatePost(int id,string userId, string title, string body){
         User? user = context.Users.Find(userId);
       if(user != null) {
            List<Post>? posts = context.Posts.Where(post => post.User.Id == user.Id && post.Id == id).ToList();
             
            if (posts != null){
                Post post = posts[0];

                post.Title = title;
                post.Body = body;
                context.Posts.Update(post);
                context.SaveChanges();
                return post;
        
             }
              throw new ArgumentException("cant find post");
       } 
         
         
        throw new ArgumentException("Cant find post whit this id");
    }

    public List<Post> GetAllPost(){

    var list = context.Posts.Include(p => p.Comments).ToList();     
    return list;
    }


}