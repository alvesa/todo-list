using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using todo_list.domain.domain;

namespace todo_list.infra.context
{
  public interface ITodoContext
  {
    public DbSet<User> Users { get; set; }
  }
}