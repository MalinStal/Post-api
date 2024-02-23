using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace post;
//one to many med post 

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
     public string Password { get; set; }
    public List<Post> Posts { get; set; }

    public User() { }

    public User(string name, string mail,string password, List<Post> Posts)
    {
        this.Name = name;
        this.Email = mail;
        this.Password = password;
        this.Posts = Posts;
    }
}



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
        this.Posts = user.Posts.Select(power => new PostDto(power)).ToList();
    }
}
