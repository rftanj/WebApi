using Microsoft.EntityFrameworkCore;
using WebAPI.Models;
using WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//var connectionString = "Server=localhost;Database=MovieApi;Uid=root;Pwd=;";

// Replace 'YourDbContext' with the name of your own DbContext derived class.
builder.Services.AddDbContext<AppDbContext>(
    dbContextOptions => { dbContextOptions
         .UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

builder.Services.AddScoped<ApplicationService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
