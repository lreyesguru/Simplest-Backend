using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using Simplest.Backend.API;
using Simplest.Backend.API.Application;
using Simplest.Backend.API.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddApplicationServices();

builder.Services.AddInfrastructureServices(config["ConnectionStrings:Default"] ?? "");

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("simplest_token", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "API Key necesario en el header 'simplest'. Ejemplo: simplest: TU_API_KEY_AQUI",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Name = "simplest_token",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Scheme = "ApiKeyScheme"
    });

    // Aplica el esquema a todos los endpoints
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "simplest_token"
                },
                Scheme = "ApiKeyScheme",
                Name = "simplest_token",
                In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            },
            new List<string>()
        }
    });

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

app.UseMiddleware<SimplestAuthorizationMiddleware>();

app.Run();