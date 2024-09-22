using Microsoft.EntityFrameworkCore;
using ResourcePlanner.Core;
using ResourcePlanner.Core.Repositories;
using ResourcePlanner.Core.UnitOfWork;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace ResourcePlanner.Api;

public class Program
{
    protected Program()
    {
    }

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();

        // Add services to the container.
        builder.Services.AddControllers();

        // Register DbContext
        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ResourcePlanner")));

        // Register the repositories and unit of work
        builder.Services.AddScoped<IResourceRepository, ResourceRepository>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Register Swagger generator
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ResourcePlanner API V1");
                c.RoutePrefix = string.Empty;  // Makes Swagger UI available at the root path
            });
        }

        app.MapDefaultEndpoints();
        app.MapControllers();

        app.Run();
    }
}
