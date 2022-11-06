namespace todo_list.domain.domain
{
  public class User : ICommonService, ICommonRepository
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime RegisteredDate { get; private set; } = DateTime.UtcNow;
  }
}