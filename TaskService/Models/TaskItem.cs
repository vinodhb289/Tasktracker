namespace TaskService.Models;
public class TaskItem
{
    public int Id {get;set;}
    public string Title {get;set;} = string.Empty;
    public string? Description{get;set;}
    public int AssignedEmployeeId {get;set;}
    public string Status{get;set;} = "Pending";
    public DateTime Duedate {get;set;}
    public DateTime CreatedAt {get;set;}
}