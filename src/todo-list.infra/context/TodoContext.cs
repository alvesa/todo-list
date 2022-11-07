using Microsoft.EntityFrameworkCore;
using todo_list.Domain.Domain;

namespace todo_list.Infra.Context
{
  public class TodoContext : DbContext, ITodoContext
  {
    public TodoContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
  }
}