using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;

namespace post;


public class CreateCommentDto
{
  public string Title {get; set;} = "";
  public string Body {get; set;}   = "";
  public int User { get; set;} =0;
  public int Post { get; set;} =0;

  
}

public class CommentDto{
  public int Id {get; set;}
 public string Title {get; set;}
  public string Body {get; set;}  

  public int User {get; set;}

  public CommentDto( Comment comment){
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
        [HttpDelete("delete/{id}")]
       public IActionResult RemoveComment(int id){
        try{
            Comment comment = commentService.RemoveComment(id);
            return Ok(comment);
        }catch(ArgumentException){
            return NotFound();
        }
       
    }
        [HttpPut("update/{id}")]
       public IActionResult UpdateComment(int id,[FromBody] CreateCommentDto dto){
        
            Comment comment = commentService.UpdateComment(id, dto.Title, dto.Body);
           if(comment != null){
               return Ok(comment);
           }
            return NotFound("cant find the comment");
        
       
    }
}