using Microsoft.EntityFrameworkCore;
using todo_list.domain.domain;

namespace todo_list.infra.context
{
  public class TodoContext : DbContext, ITodoContext
  {
    public TodoContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
  }
}