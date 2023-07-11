using System.ComponentModel.DataAnnotations;
using TodoListApp.Enums;

namespace TodoListApp.Models
{
    public class Todo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set;}
        public DateTime TimeToDo { get; set;}
        public Status Status { get; set; }
    }
}
