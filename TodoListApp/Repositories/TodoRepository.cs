using Microsoft.EntityFrameworkCore;
using TodoListApp.Data;
using TodoListApp.Enums;
using TodoListApp.Models;
using TodoListApp.ViewModels;
using TodoListApp.ViewModels.Seedwork;

namespace TodoListApp.Repositories
{
    public class TodoRepository : IToDoRepository
    {
        private readonly DataContext _context;

        public TodoRepository(DataContext dataContext)
        {
            _context = dataContext;
        }
        public async Task<Todo> CreateTask(Todo task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<Todo> DeleteTask(Todo task)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return task;
        }

        /*public async Task<List<Todo>> GetTasks()
		{
            return await _context.Tasks.ToListAsync();
		}*/

        public async Task<PagedList<Todo>> GetTasks(TaskListSearch filter)
        {
            const int pageSize = 5;
            filter.PageSize = pageSize;
            var query = _context.Tasks.AsQueryable();

            if (!string.IsNullOrEmpty(filter.StringSearch))
                query = query.Where(x => x.Title.Contains(filter.StringSearch));

            if (filter.Status.HasValue)
                query = query.Where(x => x.Status == filter.Status.Value);

            if (filter.FilterOrder.HasValue)
            {
                if(filter.FilterOrder.Value == 0)
					query = query.Where(x => x.TimeToDo.Date == DateTime.Now.Date);
                
                else
                    query = query.OrderByDescending(x => x.TimeToDo);
            }

            int recsCount = await query.CountAsync();

            int recSkip = (filter.PageNumber - 1) * filter.PageSize;

            var data = await query.Skip(recSkip).Take(filter.PageSize).ToListAsync();

            return new PagedList<Todo>(data,recsCount,filter.PageNumber,filter.PageSize);

        }



        public async Task<Todo> GetTasksById(int id)
        {
            return await _context.Tasks.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Todo> UpdateTask(Todo task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
            return task;
        }

       
    }
}
