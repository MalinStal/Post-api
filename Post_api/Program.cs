using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;

namespace post;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Auth auth = new Auth();

        // auth.policy(args);
  builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy(
                "create-post",
                policy =>
                {
                    policy.RequireAuthenticatedUser();
                }
            );
            options.AddPolicy(
                "remove-post",
                policy =>
                {
                    policy.RequireAuthenticatedUser();
                }
            );
            options.AddPolicy(
                "update-post",
                policy =>
                {
                    policy.RequireAuthenticatedUser();
                }
            );
            options.AddPolicy(
                "create-comment",
                policy =>
                {
                    policy.RequireAuthenticatedUser();
                }
            );
            options.AddPolicy(
                "delete-comment",
                policy =>
                {
                    policy.RequireAuthenticatedUser();
                }
            );
            options.AddPolicy(
                "update-comment",
                policy =>
                {
                    policy.RequireAuthenticatedUser();
                }
            );
        });
        builder.Services.AddDbContext<DatabaseContext>(options =>
            options.UseNpgsql("Host=localhost;Database=post;Username=postgres;Password=post")
        );

        //för att aktivera tokens systemet.
        builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
        //för att använda och koppla identityCore till databasen
        builder
            .Services.AddIdentityCore<User>()
            .AddEntityFrameworkStores<DatabaseContext>()
            .AddApiEndpoints();
        builder.Services.AddControllers();

        builder.Services.AddScoped<PostService, PostService>();
        builder.Services.AddScoped<CommentService, CommentService>();
         builder.Services.AddScoped<FileService, FileService>();
        

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.MapIdentityApi<User>();
  
        app.MapControllers();
        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.Run();
    }
}

class Auth
{
    public void policy(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy(
                "create-post",
                policy =>
                {
                    policy.RequireAuthenticatedUser();
                }
            );
            options.AddPolicy(
                "remove-post",
                policy =>
                {
                    policy.RequireAuthenticatedUser();
                }
            );
            options.AddPolicy(
                "update-post",
                policy =>
                {
                    policy.RequireAuthenticatedUser();
                }
            );
            options.AddPolicy(
                "create-comment",
                policy =>
                {
                    policy.RequireAuthenticatedUser();
                }
            );
            options.AddPolicy(
                "delete-comment",
                policy =>
                {
                    policy.RequireAuthenticatedUser();
                }
            );
            options.AddPolicy(
                "update-comment",
                policy =>
                {
                    policy.RequireAuthenticatedUser();
                }
            );
        });
    }
}
