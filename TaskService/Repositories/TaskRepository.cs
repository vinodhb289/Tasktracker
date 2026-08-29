// TaskService/Repositories/TaskRepository.cs
using Microsoft.EntityFrameworkCore;
using TaskService.Data;
using TaskService.Models;

namespace TaskService.Repositories;

public class TaskRepository(TaskDbContext db) : ITaskRepository
{
    public async Task<TaskItem?> GetByIdAsync(int id) =>
        await db.Tasks.FindAsync(id);

    public async Task<IEnumerable<TaskItem>> GetAllAsync(string? status = null, int? employeeId = null)
    {
        var query = db.Tasks.AsQueryable();

        if (!string.IsNullOrEmpty(status))
            query = query.Where(t => t.Status == status);

        if (employeeId.HasValue)
            query = query.Where(t => t.AssignedEmployeeId == employeeId.Value);

        return await query.OrderByDescending(t => t.CreatedAt).ToListAsync();
    }

    public async Task<TaskItem> CreateAsync(TaskItem task)
    {
        db.Tasks.Add(task);
        await db.SaveChangesAsync();
        return task;
    }

    public async Task UpdateStatusAsync(int id, string newStatus)
    {
        var task = await db.Tasks.FindAsync(id);
        if (task is null) return;

        task.Status = newStatus;
        await db.SaveChangesAsync();
    }
}