using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;


namespace post;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy(
                "login",
                policy =>
                {
                    policy.RequireAuthenticatedUser();
                }
            );
            // options.AddPolicy(
            //     "create-post",
            //     policy =>
            //     {
            //         policy.RequireAuthenticatedUser();
            //     }
            // );
           
        });
        builder.Services.AddDbContext<DatabaseContext>(options =>
            options.UseNpgsql("Host=localhost;Database=post;Username=postgres;Password=post")
        );

        //för att aktivera tokens systemet.
        builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
        //för att använda och koppla identityCore till databasen
        builder
            .Services.AddIdentityCore<User>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<DatabaseContext>()
            .AddApiEndpoints();
        builder.Services.AddControllers();

        builder.Services.AddScoped<PostService, PostService>();
        builder.Services.AddScoped<CommentService, CommentService>();
         builder.Services.AddScoped<FileService, FileService>();
        
        builder.Services.AddTransient<IClaimsTransformation, MyClaimsTransformation>();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.MapIdentityApi<User>();
  
        app.MapControllers();
        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        using(var scope = app.Services.CreateScope()){
            var RoleManager =scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var roles  = new[] {"admin", "Maneger", "Member"};
            foreach(var role in roles){
                if(!await RoleManager.RoleExistsAsync(role))
                await RoleManager.CreateAsync(new IdentityRole(role));
            }
        }

        app.Run();
    }
}
public class MyClaimsTransformation : IClaimsTransformation
{
    UserManager<User> userManager;

    public MyClaimsTransformation(UserManager<User> userManager)
    {
        this.userManager = userManager;
    }

    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        ClaimsIdentity claims = new ClaimsIdentity();

        var id = principal.FindFirstValue(ClaimTypes.NameIdentifier);
        if (id != null)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                var userRoles = await userManager.GetRolesAsync(user);
                foreach (var userRole in userRoles)
                {
                    claims.AddClaim(new Claim(ClaimTypes.Role, userRole));
                }
            }
        }

        principal.AddIdentity(claims);
        return await Task.FromResult(principal);
    }
}