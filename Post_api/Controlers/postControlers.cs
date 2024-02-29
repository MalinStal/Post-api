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
    public IActionResult CreatePost([FromBody] CreatePostDto dto)
    {
        try
        {
            Post newPost = postService.CreatePost(dto.Title, dto.Body, dto.User);
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
    public List<Post> GetAllPosts()
    {
        var list = postService.GetAllPost();
        var newList = list.Select(comment => new PostDto(comment)).ToList();
        return list;
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
