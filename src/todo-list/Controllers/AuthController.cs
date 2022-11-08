using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using todo_list.Domain.Service;
using todo_list.HttpRequest;

namespace todo_list.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class AuthController : ControllerBase
  {
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
      _authService = authService;
    }

    [HttpPost]
    [AllowAnonymous]
    public IActionResult Auth([FromBody] AuthRequest request)
    {
      if (request == null)
        return BadRequest();

      return Ok(_authService.GetToken(request.Email, request.Password));
    }
  }
}