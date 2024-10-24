using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Application.Mappings;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Application.UseCases;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
    
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add repository and use case
builder.Services.AddScoped<IUserTaskRepository, UserTaskRepository>();
builder.Services.AddScoped<UserTaskUseCase>();

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(UserTaskProfile)); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/", (context) =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});

app.Run();
