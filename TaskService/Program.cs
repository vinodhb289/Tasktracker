using Microsoft.EntityFrameworkCore;
using TaskService.Data;
using TaskService.Repositories;
using TaskService.Events;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TaskDbContext>(
    opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("TaskDb"))
);
builder.Services.AddScoped<ITaskRepository,TaskRepository>();
//builder.Services.AddSingleton<TaskEventPublisher>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
