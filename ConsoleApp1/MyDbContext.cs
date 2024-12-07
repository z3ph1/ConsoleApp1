using ConsoleApp1;
using Microsoft.EntityFrameworkCore;

public class MyDbContext : DbContext
{
    public virtual DbSet<Entity> Entities { get; set; }

    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

    public virtual Task BeginTransactionAsync()
    {
        return Task.CompletedTask;
    }

    public virtual Task RollbackTransactionAsync()
    {
        return Task.CompletedTask;
    }

    public virtual Task CommitTransactionAsync()
    {
        return Task.CompletedTask;
    }
}