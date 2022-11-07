using Microsoft.EntityFrameworkCore;
using todo_list.Domain.Domain;

namespace todo_list.Infra.Context
{
  public interface ITodoContext
  {
    public DbSet<User> Users { get; set; }
  }
}