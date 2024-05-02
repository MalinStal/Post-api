using System.IO.Compression;
using Microsoft.EntityFrameworkCore;

namespace post;

public class PostService
{
    private DatabaseContext context;

    private FileService fileService;

    public PostService(DatabaseContext context, FileService fileService)
    {
        this.context = context;
        this.fileService = fileService;
    }

    public Post CreatePost(string title, string body, string userId)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("title must not be null or empty");
        }

        if (string.IsNullOrWhiteSpace(body))
        {
            throw new ArgumentException("content must not be null or empty");
        }

        User? user = context.Users.Find(userId);

        if (user == null)
        {
            throw new ArgumentException("there is no User connected to this id");
        }

        Post newPost = new Post(title, body, user, new List<Comment>());

     
        context.Posts.Add(newPost);
        context.SaveChanges(); 

        return newPost;
     }
    //  public Post DeletePost(int id, string userId)
    // {
    //test med hjälp v chattGTP 
    //     User? user = context.Users.Find(userId);
    //     if (user == null)
    //     {
    //         throw new ArgumentException("User is not logged in");
    //     }
    //     int postId = id;
    //   Post? post = context.Posts
    // .Include(p => p.Comments)
    // .Include(p => p.Images)
    // .FirstOrDefault(p => p.Id == id && p.User.Id == user.Id);
           
    //     if (post == null)
    //     {
    //         throw new ArgumentException("Post not found");
    //         Console.WriteLine("1"  + postId+ id + post.Id + post.Images?[0].Id + post.Comments?[0].Id);

    //     } 
    //     Console.WriteLine("1"  + postId+ id + post.Id + post.Images?[0].Id + post.Comments?[0].Id);
    //     foreach (var comment in post.Comments)
    //     {
    //         commentService.RemoveComment(comment.Id, userId);
    //           Console.WriteLine("2" + comment.Id);
    //     }

    //     foreach (var file in post.Images)
    //     {
    //         fileService.DeleteFile(post.Id, userId, file.Id);
    //           Console.WriteLine("3" + file.Id);
    //     }

    //     context.Posts.Remove(post);
    //     context.SaveChanges();

    //     return post;
    // }

    public Post DeletePost(int id, string userId)
    {
        User? user = context.Users.Find(userId);
        if (user != null)
        {
            List<Post>? posts = context
                .Posts.Where(post => post.User.Id == user.Id && post.Id == id)
                .ToList();

            if (posts != null)
            {
                Post post = posts[0];
                if(post.Comments != null){
                    Console.WriteLine("commetns not null");
                }
                 if(post.Images != null){
                    Console.WriteLine("images not null");
                }
                context.Posts.Remove(post);
                context.SaveChanges();
                return post;
            }
            throw new ArgumentException("cant find post");
        }
        throw new ArgumentException("cant find post");
    }

    public Post UpdatePost(int id, string userId, string title, string body)
    {
        User? user = context.Users.Find(userId);
        if (user != null)
        {
            List<Post>? posts = context
                .Posts.Where(post => post.User.Id == user.Id && post.Id == id)
                .ToList();

            if (posts != null)
            {
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

    public List<Post> GetAllPost()
    {
        
        var list = context.Posts.Include(p => p.Comments).Include(p => p.Images).ToList();
        foreach(var item in list){
            Console.WriteLine(item.Images.Count);
        }
        return list;

    }
}
