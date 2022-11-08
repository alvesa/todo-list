using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace todo_list.Domain.Service
{
  public class AuthService : IAuthService
  {
    private readonly IConfiguration _configuration;
    private readonly IUserService _user;
    public string Secret { get; private set; }
    public SymmetricSecurityKey Key { get; private set; }

    public AuthService(IConfiguration configuration, IUserService user)
    {
      _configuration = configuration;
      _user = user;
      Initialize();
    }

    private void Initialize() 
    {
      Secret = _configuration["JwtSettings:secret"];
      SetKey(Secret);
    }

    public string GetToken(string email, string password)
    {
      return CreateToken(email, password);
    }

    private string CreateToken(string email, string password)
    {
      var handler = new JwtSecurityTokenHandler();
      var user = _user.Get(x => x.Email == email && x.Password == password);

      if(user == null)
        throw new ArgumentException("Invalid credentials");

      var token = GetSecurityToken(user.Name, user.Email);
      return handler.WriteToken(token);
    }

    private JwtSecurityToken GetSecurityToken(string name, string email)
    {
      var credentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256Signature);

      return new JwtSecurityToken(
        claims: new List<Claim> {
          new Claim("name", name),
          new Claim("email", email),
        },
        signingCredentials: credentials,
        expires: DateTime.Now.AddMinutes(10)
      );
    }

    private void SetKey(string secret) => Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
  }
}