using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace post;
//one to many med post 

public class User : IdentityUser
{
    public List<Post>? Posts { get; set; }

    public User() { }

    // public User(List<Post> post){

    // }

   
}

