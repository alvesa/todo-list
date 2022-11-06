namespace todo_list.domain
{
  public interface IRepository<T> where T : ICommonRepository
  {
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task<Guid> AddAsync(T value);
    Task UpdateAsync(Guid id, T value);
    Task DeleteAsync(Guid id);
  }
}