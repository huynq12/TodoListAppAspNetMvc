using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Reflection;
using System.Text.Json.Serialization;
using TodoListApp.Enums;
using TodoListApp.Models;
using TodoListApp.Repositories;

using TodoListApp.ViewModels;
using TodoListApp.ViewModels.Seedwork;

namespace TodoListApp.Controllers
{
    public class TodoController : Controller
    {
        private readonly IToDoRepository _taskRepository;

        public TodoController(IToDoRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public async Task<IActionResult> ListTask( TaskListSearch filter,int pg=1)
        {
			filter.PageNumber = pg;

			ViewBag.Filter = filter;

			PagedList<Todo> result = await _taskRepository.GetTasks(filter);

			return View(result);
        }
		

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create([Bind("Title,Description,TimeToDo")] Todo todo)
		{
			if (!ModelState.IsValid)
			{
				return View(todo);
			}
			todo.CreatedDate = DateTime.Now;
			todo.Status = Enums.Status.Todo;
			await _taskRepository.CreateTask(todo);
			
			return RedirectToAction("ListTask","Todo");
		}

		public async Task<ActionResult> Detail(int id) {
			var task = await _taskRepository.GetTasksById(id);
			if (task == null)
			{
				return View(task);
			}
			return View(task);
		}

		public async Task<IActionResult> UpdateDo(int id)
		{
			var existingTask = await _taskRepository.GetTasksById(id);
			if(existingTask == null)
			{
				return View(existingTask);
			}
			if(existingTask.Status == Enums.Status.Todo)
			{
				existingTask.Status = Enums.Status.Doing;
				await _taskRepository.UpdateTask(existingTask);
				return RedirectToAction("ListTask", "Todo");
			}
			return View(existingTask);
		}
		public async Task<IActionResult> UpdateDone(int id)
		{
			var existingTask = await _taskRepository.GetTasksById(id);
			if (existingTask == null)
			{
				return View(existingTask);
			}
			if(existingTask.Status == Enums.Status.Doing || existingTask.Status == Enums.Status.Todo)
			{
				existingTask.Status = Enums.Status.Done;
				await _taskRepository.UpdateTask(existingTask);
				return RedirectToAction("ListTask", "Todo");
			}
			return View(existingTask);
		}

		public async Task<IActionResult> Delete(int id)
		{
			var delTask = await _taskRepository.GetTasksById(id);
			if(delTask == null)
			{
				return View(delTask);
			}
            else
            {
                 await _taskRepository.DeleteTask(delTask);
				return RedirectToAction("ListTask", "Todo");
            }
        }

	}
}
