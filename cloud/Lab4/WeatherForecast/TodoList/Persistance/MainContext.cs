using Microsoft.EntityFrameworkCore;
using TodoList.Core;

namespace TodoList.Persistance;

public class MainContext(DbContextOptions<MainContext> options) : DbContext(options)
{
    public DbSet<TodoItem> TodoItems { get; set; } = null!;
}
