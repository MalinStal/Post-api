using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;





namespace post;

[ApiController]
[Route("post")]
public class PostControllers : ControllerBase
{
    PostService postService;
    FileService fileService;
     private RoleManager<IdentityRole> roleManager;
    private UserManager<User> userManager;

    public PostControllers(PostService postService, FileService fileService)
    {
        this.postService = postService;
        this.fileService = fileService;
    }

    [HttpPost("newpost")]
    [Authorize("login")]
    //[Authorize("create-post")]
    public IActionResult CreatePost([FromBody] CreatePostDto dto)
    {
        try
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (id == null)
            {
                return BadRequest();
            }

            
                Post newPost = postService.CreatePost(dto.Title, dto.Body, id);
                PostDto output = new PostDto(newPost);
                return Ok(output);
            
        }
        catch (ArgumentException)
        {
            return BadRequest();
        }
    }

    [HttpDelete("delete/{id}")]
     [Authorize("login")]
    //[Authorize("delete-post")]
    public IActionResult DeletePost(int id)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
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
    public List<PostDto> GetAllPosts()
    {
        var list = postService.GetAllPost();
        var newList = list.Select(post => new PostDto(post)).ToList();
        return newList;
    }

    [HttpPut("update/{id}")]
    //[Authorize("update-post")]
     [Authorize("login")]
    public IActionResult UpdatePost(int id, [FromBody] CreatePostDto dto)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                Post post = postService.UpdatePost(id, userId, dto.Title, dto.Body);
                if (post != null)
                {
                    return Ok(post);
                }
            }
        }
        catch (ArgumentException)
        {
            return BadRequest("you are not authorized");
        }

        return BadRequest("you are not authorized");
    }
}
