using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace TodoListApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> option):base(option)
        {
            
        }
        public DbSet<Models.Todo> Tasks { get; set; }
    }
}
