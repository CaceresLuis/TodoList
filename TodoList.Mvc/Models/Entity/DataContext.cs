using Microsoft.EntityFrameworkCore;

namespace TodoList.Mvc.Models.Entity
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Todo> Todos { get; set; }
    }
}
