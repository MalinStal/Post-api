using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
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
    FileService fileService;

    public PostControllers(PostService postService, FileService fileService)
    {
        this.postService = postService;
        this.fileService = fileService;
    }

    [HttpPost("newpost")]
    [Authorize("create-post")]
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
    [Authorize("remove-post")]
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
    public List<Post> GetAllPosts()
    {
        var list = postService.GetAllPost();
        //behövs denna rad ens??
        var newList = list.Select(comment => new PostDto(comment)).ToList();
        return list;
    }

    [HttpPut("update/{id}")]
    [Authorize("update-post")]
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
