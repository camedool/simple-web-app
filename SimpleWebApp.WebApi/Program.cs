using Microsoft.EntityFrameworkCore;
using SimpleWebApp.WebApi.Data;
using SimpleWebApp.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);
var devPolicy = "developer";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSqlite(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddAppServices();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: devPolicy,
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:8080")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                      });
});

var app = builder.Build();

// apply db migration
using var scrope = app.Services.CreateScope();
using var context = scrope.ServiceProvider.GetRequiredService<AppDbContext>();
context.Database.Migrate();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(devPolicy);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
