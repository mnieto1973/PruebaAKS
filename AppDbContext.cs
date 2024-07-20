
using Microsoft.EntityFrameworkCore;



    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
        public DbSet<TodoItem> TodoItems { get; set; }
    }
    public class TodoItem
    {
        public int Id { get; set; } 
        public string Nombre { get; set; }
    }