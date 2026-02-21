using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Notes.Api.Configuration;
using Scalar.AspNetCore;
using Notes.Api.Services;
using Notes.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddOpenApi();

Env.Load();

var connectionString = Environment.GetEnvironmentVariable("NOTES_DB_CONNECTION")
                       ?? throw new InvalidOperationException("Connection string not set");

var allowedOrigins = builder.Configuration
    .GetSection("Cors:AllowedOrigins")
    .Get<string[]>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(General.CorsPolicy, policy =>
    {
        policy.WithOrigins(allowedOrigins!)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddScoped<IFilesService, FilesService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}



app.UseCors(General.CorsPolicy);
app.UseHttpsRedirection();
app.MapControllers();
app.Run();