using TodoListApp.Models;
using TodoListApp.ViewModels;
using TodoListApp.ViewModels.Seedwork;

namespace TodoListApp.Repositories
{
    public interface IToDoRepository
    {
        Task<PagedList<Todo>> GetTasks(TaskListSearch filter);
        Task<Models.Todo> GetTasksById(int id);
        Task<Models.Todo> CreateTask(Models.Todo task);
        Task<Models.Todo> UpdateTask(Models.Todo task);
        Task<Models.Todo> DeleteTask(Models.Todo task);
    }
}
