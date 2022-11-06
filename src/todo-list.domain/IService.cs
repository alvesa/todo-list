namespace todo_list.domain
{
  public interface IService<T> where T : ICommonService
  {
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task<Guid> AddAsync(T value);
    Task UpdateAsync(Guid id, T value);
    Task DeleteAsync(Guid id);
  }
}