using Microsoft.EntityFrameworkCore;

namespace post;

public class DatabaseContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options) { }
}



