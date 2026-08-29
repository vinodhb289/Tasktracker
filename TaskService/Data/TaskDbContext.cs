using Microsoft.EntityFrameworkCore;
using TaskService.Models;

namespace TaskService.Data;

public class TaskDbContext(DbContextOptions<TaskDbContext> options) : DbContext(options)
{
    public DbSet<TaskItem> Tasks => Set<TaskItem>();
    public DbSet<Employee> Employees => Set<Employee>();
}