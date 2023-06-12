using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ToDoList.Database.Models;

namespace ToDoList.Database;

public class AppDbContext : DbContext
{
    public DbSet<ToDoListDb> ToDoLists => Set<ToDoListDb>();

    public DbSet<ToDoListItemDb> ToDoListItems => Set<ToDoListItemDb>();
    
    public AppDbContext(DbContextOptions<AppDbContext> _options, ILogger<AppDbContext> logger)
    {
        // try
        // {
        //     Database.EnsureCreated();
        // }
        // catch (SqliteException) { logger.LogInformation("Database already created"); }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Filename=MyDatabase.db");
    }
}