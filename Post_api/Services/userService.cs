using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace post;

public class UserService{
    
    private DatabaseContext context;
    public UserService(DatabaseContext context){
         this.context = context;
    }

    public User CreateUser(string name, string email, string password){
      if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("UserName must not be null or empty");
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Email must not be null or empty");
        }
          if (string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentException("password must not be null or empty");
        }
        User newUser = new User(name,email,password, new List<Post>());
        context.Users.Add(newUser);
        context.SaveChanges();
        return newUser;
    }

    public User DeleteUser(int id){
       
             User? user = context.Users.Find(id);
             if (user != null){
                context.Users.Remove(user);
                context.SaveChanges();
                return user;
        
             }
            throw new ArgumentException("cant find user");
         
       
    }

    public User UpdateUser(int id, string email){
        User? user = context.Users.Find(id);
        if(user != null ){
           user.Email = email;
           context.SaveChanges();
           return user;
        }
        throw new ArgumentException("Cant find User whit this id");
    }

    public List<User> GetAllUsers(){
        //ta användaren och inkludera listan som heter AllMyPost
        var list = context.Users.Include(list => list.Posts).ToList();
        return list;

    }


}