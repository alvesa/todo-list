using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using todo_list.domain.Enum;

namespace todo_list.Domain.Service
{
  public class AuthService : IAuthService
  {
    private readonly IConfiguration _configuration;
    private readonly IUserService _user;
    public SymmetricSecurityKey Key { get; private set; }

    public AuthService(IConfiguration configuration, IUserService user)
    {
      _configuration = configuration;
      _user = user;
      Key = GetKey(_configuration["JwtSettings:secret"]);
    }

    public string GetToken(string email, string password)
    {
      return CreateToken(email, password);
    }

    public bool IsTokenValid(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = GetValidationParameters();

        var principalClaims = tokenHandler.ValidateToken(token.Split(' ').Last(), validationParameters, out SecurityToken validatedToken);
        return true;
    }

    private TokenValidationParameters GetValidationParameters()
    {
        return new TokenValidationParameters()
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = Key,
          ValidateIssuer = false,
          ValidateAudience = false,
        };
    }

    private string CreateToken(string email, string password)
    {
      var handler = new JwtSecurityTokenHandler();
      var user = _user.Get(x => x.Email == email && x.Password == password);

      if(user == null)
        throw new ArgumentException("Invalid credentials");

      var token = GetSecurityToken(user.Name, user.Email, user.Role);
      return handler.WriteToken(token);
    }

    private JwtSecurityToken GetSecurityToken(string name, string email, Role role)
    {
      var credentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256Signature);

      return new JwtSecurityToken(
        claims: new List<Claim> {
          new Claim("name", name),
          new Claim("email", email),
          new Claim("role", role.ToString()),
        },
        signingCredentials: credentials,
        expires: DateTime.Now.AddMinutes(10)
      );
    }

    private SymmetricSecurityKey GetKey(string secret) {
     return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
    }
  }
}