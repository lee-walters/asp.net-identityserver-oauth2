using Microsoft.EntityFrameworkCore;

namespace TestMVCApi.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> contextOptions) : base(contextOptions)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}