using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;

namespace post;


public class CreateUserDto
{
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
     public string Password { get; set; } = "";
    public List<CreatePostDto> Posts { get; set; } = new List<CreatePostDto>();
}

public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }

     public string Password { get; set; } 
    public List<PostDto> Posts { get; set; }

    public UserDto(User user)
    {
        this.Id = user.Id;
        this.Name = user.Name;
        this.Email= user.Email;
        this.Password = user.Password;
        this.Posts = user.Posts.Select(post => new PostDto(post)).ToList();
    }
}


[ApiController]
[Route("user")]
public class UserControllers : ControllerBase
{
    UserService userService;

    public UserControllers(UserService userService)
    {
        this.userService = userService;
    }

    [HttpPost]
    public IActionResult CreateUser([FromBody] CreateUserDto dto)
    {
        try
        {
            User newUser = userService.CreateUser(dto.Name, dto.Email, dto.Password);
            return Ok(newUser);
        }
        catch (ArgumentException)
        {
            return BadRequest();
        }
    }

    [HttpDelete("delete/{id}")]
    public IActionResult DeleteUser(int id)
    {
        try
        {
            User user = userService.DeleteUser(id);
            return Ok(user);
        }
        catch (ArgumentException)
        {
            return BadRequest();
        }
    }

    [HttpGet]
    public List<User> GetAllUser()
    {   var list = userService.GetAllUsers();
         var newList = list.Select(post => new UserDto(post)).ToList();
        return list;
      
    }

    [HttpPut("update/{id}")]
    public IActionResult UpdateUser(int id, [FromBody] string mail)
    {
        User user = userService.UpdateUser(id, mail);
        if (user != null)
        {
            return Ok(user);
        }
        return NotFound();
    }
}
