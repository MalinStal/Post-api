using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;

namespace post;

[ApiController]
[Route("comment")]
public class CommentControllers : ControllerBase
{
    CommentService commentService;
    public CommentControllers(CommentService commentService){
        this.commentService = commentService;
    }
    [HttpPost]
    public IActionResult AddComment([FromBody] CreateCommentDto dto){
        try{
            Comment comment = commentService.AddComment(dto.Title, dto.Body, dto.User, dto.Post);
            return Ok(comment);
        }catch(ArgumentException){
            return BadRequest();
        }
       
    }
    
}