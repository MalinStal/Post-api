using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;


namespace post;

[ApiController]
[Route("post")]
public class RolesController : ControllerBase
{
   
     private RoleManager<IdentityRole> roleManager;
    private UserManager<User> userManager;

    public RolesController( RoleManager<IdentityRole> roleManager,
        UserManager<User> userManager)
    {
       this.roleManager = roleManager;
        this.userManager = userManager;
    }

    // [HttpPost("newpost")]
    // [Authorize("login")]
    // //[Authorize("create-post")]
    // public IActionResult CreatePost([FromBody] CreatePostDto dto)
    // {
    //     try
    //     {
    //         var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
    //         if (id == null)
    //         {
    //             return BadRequest();
    //         }

            
    //             Post newPost = postService.CreatePost(dto.Title, dto.Body, id);
    //             PostDto output = new PostDto(newPost);
    //             return Ok(output);
            
    //     }
    //     catch (ArgumentException)
    //     {
    //         return BadRequest();
    //     }
    // }



}
