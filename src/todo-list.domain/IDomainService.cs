namespace todo_list.Domain
{
  public interface IDomainService<T>
  {
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    T Get(Func<T, bool> predicate);
    Task<Guid> AddAsync(T value);
    Task UpdateAsync(Guid id, T value);
    Task DeleteAsync(Guid id);
  }
}