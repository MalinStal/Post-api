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


    
}