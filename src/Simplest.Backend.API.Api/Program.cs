using Microsoft.OpenApi.Models;
using Simplest.Backend.API.Application;
using Simplest.Backend.API.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(config["ConnectionStrings:Default"] ?? "");

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = config["Documentation:Title"],
        Version = config["Documentation:Version"],
        Description = config["Documentation:Description"],
        Contact = new OpenApiContact
        {
            Name = config["Documentation:Contact:Name"],
            Email = config["Documentation:Contact:Email"]
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.MapGet("/", context =>
    {
        context.Response.Redirect("/swagger");
        return Task.CompletedTask;
    });
}

app.MapControllers();
// app.UseHttpsRedirection();
app.Run();