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

    public User(string name, string mail, string password, List<Post> Posts)
    {
        this.Name = name;
        this.Email = mail;
        this.Password = password;
        this.Posts = Posts;
    }
}
