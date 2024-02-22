using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;

namespace post;

[ApiController]
[Route("user")]
public class UserControllers : ControllerBase
{
    UserService userService;

    public UserControllers(UserService userService)
    {
        this.userService = userService;
    }

    [HttpPost("newuser")]
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

    [HttpGet("users")]
    public List<User> GetAllUser()
    {
        return userService.GetAllUsers();
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
