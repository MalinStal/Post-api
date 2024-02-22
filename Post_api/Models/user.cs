using Microsoft.EntityFrameworkCore;

namespace post;

public class User {
    public int Id {get; set;}
    public string Name {get; set;}
    public string Password {get; set;}
    public string Email {get; set;}

    List<Post> AllMyPost {get; set;}
    public User(){}
    public User(string name, string email, string password) {
        Name =name;
        Email = email;
        Password = password;
        AllMyPost = new List<Post>();

    }
    

}


  public class CreateUserDto
{
    public string Name { get; set; } 
    public string Password {get; set;} 
    public string Email { get; set; } 

  
    public CreateUserDto(){}

    
    public CreateUserDto (string name, string mail, string password){
        this.Name = name;
        this.Email = mail;
        this.Password = password;
    
    }
}

