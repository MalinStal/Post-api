namespace post;

public class PostService{
    
    private DatabaseContext context;
    public PostService(DatabaseContext context){
         this.context = context;
    }

     public Post CreatePost(string title, string body, int userId){
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
            Post newPost = new Post(title,body,user); 
            context.Posts.Add(newPost);
           
            context.SaveChanges();
            return newPost;
        }
      throw new ArgumentException("there is no User connected to this id");
       
    }

    public Post DeletePost(int id){
       
             Post? post = context.Posts.Find(id);
             if (post != null){
                context.Posts.Remove(post);
                context.SaveChanges();
                return post;
        
             }
            throw new ArgumentException("cant find post");
         
       
    }

    public Post UpdatePost(int id,string title, string body){
        
             Post? post = context.Posts.Find(id);
             if (post != null){
                post.Title = title;
                post.Body = body;
                context.Posts.Update(post);
                context.SaveChanges();
                return post;
        
             }
        throw new ArgumentException("Cant find post whit this id");
    }

    public List<Post> GetAllUsers(){
        return context.Posts.ToList();
    }


}