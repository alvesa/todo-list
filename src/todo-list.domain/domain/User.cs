using todo_list.domain.Enum;

namespace todo_list.Domain.Domain
{
  public class User
  {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }
    public DateTime RegisteredDate { get; private set; } = DateTime.UtcNow;
  }
}