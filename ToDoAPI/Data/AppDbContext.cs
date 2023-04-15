using ToDoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ToDoAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(
        DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<ToDo> ToDos { get; set; }
}
