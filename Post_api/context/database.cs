using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace post;

public class DatabaseContext : IdentityDbContext<User>
{
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
       public DbSet<FileModel> FileModels { get; set; } 

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options) { }
}



