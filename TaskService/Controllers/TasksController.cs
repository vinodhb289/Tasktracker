using Microsoft.AspNetCore.Mvc;
using TaskService.Models;
using TaskService.Repositories;
using TaskService.Events;

namespace TaskService.Controllers;

[ApiController]
[Route("api/tasks")]
public class TasksController(ITaskRepository repo,TaskEventPublisher publisher):ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? status,[FromQuery] int? employeeId)
    {
        var tasks = await repo.GetAllAsync(status,employeeId);
        return Ok(tasks);
    }
}