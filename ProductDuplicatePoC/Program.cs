namespace ProductDuplicatePoC;

using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using ProductDuplicatePoC.Application;
using ProductDuplicatePoC.Data;
using ProductDuplicatePoC.Data.Sql;
using Scalar.AspNetCore;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers()
            .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi(options =>
        {
            // options.CreateSchemaReferenceId = (type) => type.Type.IsEnum ? null : OpenApiOptions.CreateDefaultSchemaReferenceId(type);
        });

        builder.Services.AddScoped<GroupService>();
        builder.Services.AddScoped<GroupRepository>();
        builder.Services.AddScoped<ProductService>();

        builder.Services.AddScoped<DuplicateGroupRepository>();
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
           options
            .UseSqlServer("Server=127.0.0.1,1433;Database=MyDatabase;User Id=sa;Password=YourPassword123;TrustServerCertificate=True;")
            .EnableSensitiveDataLogging(false));

        builder.Services.AddSingleton<IMongoClient, MongoClient>(
            sp => new MongoClient(
                "mongodb://admin:password@localhost:27017/productduplicate?authSource=admin&connectTimeoutMS=3000"));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
