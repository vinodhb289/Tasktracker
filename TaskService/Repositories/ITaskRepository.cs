using TaskService.Models;
namespace TaskService.Repositories;

public interface ITaskRepository
{
    Task<TaskItem?> GetByIdAsync(int id);
    Task<IEnumerable<TaskItem>> GetAllAsync(string? status=null,int? employeeid =null);
    Task<TaskItem> CreateAsync(TaskItem task);
    Task UpdateStatusAsync(int id,string newStatus);
}