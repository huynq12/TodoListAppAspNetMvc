using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualBasic;
using TodoListApp.Enums;
using TodoListApp.ViewModels.Seedwork;

namespace TodoListApp.ViewModels
{
    public class TaskListSearch : PagingParameters
    {
        //public string? CurrentFilter { get; set; }
        public string? StringSearch { get; set; }
        public Status? Status { get; set; }
        public FilterOrder? FilterOrder { get; set; }
        public string Selected { get; set; }

    }
}
