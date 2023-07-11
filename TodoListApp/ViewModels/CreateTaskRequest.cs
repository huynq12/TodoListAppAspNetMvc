using TodoListApp.Enums;

namespace TodoListApp.ViewModels
{
	public class CreateTaskRequest
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime TimeToDo { get; set; }
		public Status Status { get; set; }
	}
}
