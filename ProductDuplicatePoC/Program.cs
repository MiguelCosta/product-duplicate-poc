namespace ProductDuplicatePoC;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using ProductDuplicatePoC.Application;
using ProductDuplicatePoC.Data;
using Scalar.AspNetCore;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        builder.Services.AddScoped<GroupService>();
        builder.Services.AddScoped<GroupRepository>();
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
