using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;

namespace post;

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
    public IActionResult CreateUser([FromBody] CreatePostDto dto, [FromQuery] int user)
    {
        try
        {
            Post newPost = postService.CreatePost(dto.Title, dto.Body, user);
            return Ok(newPost);
        }
        catch (ArgumentException)
        {
            return BadRequest();
        }
    }

    [HttpDelete("delete/{id}")]
    public IActionResult DeletePost(int id)
    {
        try
        {
            Post post = postService.DeletePost(id);
            return Ok(post);
        }
        catch (ArgumentException)
        {
            return BadRequest();
        }
    }

    [HttpGet("allPosts")]
    public List<Post> GetAllUser()
    {
        return postService.GetAllUsers();
    }

    [HttpPut("update/{id}")]
    public IActionResult UpdatePost(int id, [FromBody] CreatePostDto dto)
    {
        Post post = postService.UpdatePost(id, dto.Title, dto.Body);
        if (post != null)
        {
            return Ok(post);
            
        }
            return NotFound();

    }
}
