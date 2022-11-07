namespace todo_list.Domain.Service
{
  public interface IAuthService
  {
    string GetToken(string email, string password);
  }
}