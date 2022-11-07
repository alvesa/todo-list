using Microsoft.EntityFrameworkCore;
using todo_list.Domain.Domain;
using todo_list.Domain.Repository;
using todo_list.Infra.Context;

namespace todo_list.Infra.Repository
{
  public class UserRepository : IUserRepository
  {
    private readonly TodoContext _context;

    public UserRepository(TodoContext context)
    {
      _context = context;
    }

    public async Task<Guid> AddAsync(User value)
    {
      var user = await _context.AddAsync(value);
      await _context.SaveChangesAsync();

      return user.Entity.Id;
    }

    public async Task DeleteAsync(Guid id)
    {
      var entity = await _context.Users.FindAsync(id);
      if (entity == null)
        return;
      
      _context
        .Set<User>()
        .Remove(entity);
      
      await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
      return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
      return await _context.Users.FindAsync(id);
    }

    public User Get(Func<User, bool> predicate)
    {
      return _context.Users.Where(predicate).FirstOrDefault();
    }

    public async Task UpdateAsync(Guid id, User value)
    {
      var entity = await _context.Users.FindAsync(id);
      if (entity == null)
        return;
      
      _context
        .Set<User>()
        .Update(entity);
      
      await _context.SaveChangesAsync();
    }
  }
}