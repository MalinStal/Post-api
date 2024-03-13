using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;

namespace post;

public class CreateCommentDto
{
    public string Title { get; set; } = "";
    public string Body { get; set; } = "";
    public int User { get; set; } = 0;
    public int Post { get; set; } = 0;
}

public class CommentDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Body { get; set; }

    public int User { get; set; }

    public CommentDto(Comment comment)
    {
        this.Id = comment.Id;
        this.Title = comment.Title;
        this.Body = comment.Body;
    }
}

[ApiController]
[Route("comment")]
public class CommentControllers : ControllerBase
{
    CommentService commentService;

    public CommentControllers(CommentService commentService)
    {
        this.commentService = commentService;
    }

    [HttpPost]
    [Authorize("create-comment")]
    public IActionResult AddComment([FromBody] CreateCommentDto dto)
    {
        try
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (id != null)
            {
                Comment comment = commentService.AddComment(dto.Title, dto.Body, id, dto.Post);
                return Ok(comment);
            }
            return NotFound();
        }
        catch (ArgumentException)
        {
            return BadRequest();
        }
    }

    [HttpDelete("delete/{id}")]
    [Authorize("delete-comment")]
    public IActionResult RemoveComment(int id)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return NotFound();
            }
            Comment comment = commentService.RemoveComment(id, userId);
            return Ok(comment);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }

    [HttpPut("update/{id}")]
    [Authorize("update-comment")]
    public IActionResult UpdateComment(int id, [FromBody] CreateCommentDto dto)
    {   var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
        Comment comment = commentService.UpdateComment(user, id, dto.Title, dto.Body);
        if (comment != null)
        {
            return Ok(comment);
        }
        return NotFound("cant find the comment");
    }
}
