using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
namespace post;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddControllers();
          builder.Services.AddDbContext<DatabaseContext>(
            options =>
                options.UseNpgsql(
                    "Host=localhost;Database=post;Username=postgres;Password=post" 
        ));

        builder.Services.AddScoped<UserService, UserService>();
        builder.Services.AddScoped<PostService, PostService>();
        // // Add services to the container.
        // builder.Services.AddAuthorization();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // // Configure the HTTP request pipeline.
        // if (app.Environment.IsDevelopment())
        // {
        //     app.UseSwagger();
        //     app.UseSwaggerUI();
        // }
        
        app.MapControllers();
        app.UseHttpsRedirection();

        // app.UseAuthorization();


        app.Run();
    }
}
