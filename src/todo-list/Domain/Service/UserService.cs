using todo_list.Domain.Domain;
using todo_list.Domain.Repository;

namespace todo_list.Domain.Service
{
  public class UserService : IUserService
  {
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
      _userRepository = userRepository;
    }

    public async Task<Guid> AddAsync(User value)
    {
      return await _userRepository.AddAsync(value);
    }

    public async Task DeleteAsync(Guid id)
    {
      await _userRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
      return await _userRepository.GetAllAsync();
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
      return await _userRepository.GetByIdAsync(id);
    }

    public User Get(Func<User, bool> predicate)
    {
      if(predicate == null) {
        throw new ArgumentNullException("");
      }
      
      return _userRepository.Get(predicate);
    }

    public async Task UpdateAsync(Guid id, User value)
    {
      await _userRepository.UpdateAsync(id, value);
    }
  }
}