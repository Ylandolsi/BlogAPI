using System.ComponentModel.Design;
using Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Repository;
using Services;
using BlogApi.Extensions.ExceptionHandlers;

namespace BlogApi.Extensions;

public static class Service
{
    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });



    public static void ConfigureArticleService ( this IServiceCollection services)=>
        services.AddScoped<IArticleSevice, ArticleService>();

    public static void ConfigureArticleRepo(this IServiceCollection services) =>
        services.AddScoped<IArticleRepository, ArticleRepository>(); 
    
    


    
    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(s =>
        {
            s.SwaggerDoc("v1", new OpenApiInfo { Title = "Blog API ", Version = "v1"
            });
            s.SwaggerDoc("v2", new OpenApiInfo { Title = "Blog API ", Version = "v2"
            });
        });
    }
    

    public static void AddMyDependencyGroup( this WebApplicationBuilder builder )
    {
        builder.Services.ConfigureCors(); 
        builder.Services.AddOpenApi(); // adds support for swagger 
        builder.Services.ConfigureSwagger();
        builder.Services.ConfigureArticleService();
        builder.Services.ConfigureArticleRepo();
        builder.Services.AddDbContext<DbContextRepository>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        );
        
        
        // these need app.UseExceptionHandler(); in the pipeline 
        
        builder.Services.AddExceptionHandler<BadRequestHandler>();
        builder.Services.AddExceptionHandler<NotFoundHandler>();
        // if the exception is not handled by the above handlers, it will be handled by the GlobalErrorHandler
        builder.Services.AddExceptionHandler<GlobalErrorHandler>();

        builder.Services.AddProblemDetails(); // adds the ProblemDetails ( class to represnt errors)   to the services collection
        

    }
    
    
    
    

  
}
