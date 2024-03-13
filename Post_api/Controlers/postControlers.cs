using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;

namespace post;

public class CreatePostDto
{
    public string Title { get; set; } = "";
    public string Body { get; set; } = "";
    public int User {get; set;} = 0;
     public int Post {get; set;} = 0;
     public List<CreateCommentDto> Comments {get; set;} = new List<CreateCommentDto>();
    public List<FileModel> Files {get; set;} = new List<FileModel>();
}

public class PostDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }

    public List<CommentDto> Comments {get; set;}
    public List<FileModel> Files {get; set;}

    public PostDto(Post post)
    {
        this.Id = post.Id;
        this.Title = post.Title;
        this.Body = post.Body;
        this.Comments = post.Comments.Select(comment => new CommentDto(comment)).ToList();
        this.Files = post.Images.Select(files => new FileModel(files.Name, files.Content, files.Extension)).ToList();
    }
}
[ApiController]
[Route("post")]
public class PostControllers : ControllerBase
{
    PostService postService;
   

    public PostControllers(PostService postService)
    {
        this.postService = postService;
    }

    [HttpPost("newpost")]
    [Authorize("create-post")]
    public IActionResult CreatePost([FromBody] CreatePostDto dto)
    {
        try
        {
           var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
           if(id != null )
           {
                Post newPost = postService.CreatePost(dto.Title, dto.Body, id);

                PostDto output = new PostDto(newPost);
            return Ok(output);
           }

         return BadRequest();

        }
        catch (ArgumentException)
        {
            return BadRequest();
        }
    }


    [HttpDelete("delete/{id}")]
    [Authorize("remove-post")]
    public IActionResult DeletePost(int id)
    {
        try
        {
             var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
             if(userId == null)
             {
                return NotFound();
             }
            Post post = postService.DeletePost(id, userId);
            return Ok(post);
        }
        catch (ArgumentException)
        {
            return BadRequest();
        }
    }

    [HttpGet("allPosts")]
    public List<Post> GetAllPosts()
    {
        var list = postService.GetAllPost();
        var newList = list.Select(comment => new PostDto(comment)).ToList();
        return list;
    }

    [HttpPut("update/{id}")]
    [Authorize("update-post")]
    public IActionResult UpdatePost(int id, [FromBody] CreatePostDto dto)
    {
        try{
             var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
          if(userId != null ){
             Post post = postService.UpdatePost(id, userId, dto.Title, dto.Body);
        if (post != null)
        {
            return Ok(post);
            
        }
          }
       
        }catch(ArgumentException){
             return BadRequest("you are not authorized");
        }
   
             return BadRequest("you are not authorized");

    }
}
