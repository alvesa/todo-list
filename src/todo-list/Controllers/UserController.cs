using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using todo_list.Domain.Domain;
using todo_list.Domain.Service;

namespace todo_list.Controllers
{
  [ApiController]
  [Route("[controller]")]
  [Authorize]
  public class UserController : ControllerBase
  {
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
      _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get() => Ok(await _service.GetAllAsync());
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id) => Ok(await _service.GetByIdAsync(id));
    
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Add([FromBody] User user)
    {
      var id = await _service.AddAsync(user);

      return Created($"user/{id}", id);
    }

    [HttpPut]
    public async Task<IActionResult> Update(Guid id, [FromBody] User user)
    {
      var userEntity = await _service.GetByIdAsync(id);

      if (userEntity == null)
        return NotFound();

      await _service.UpdateAsync(id, userEntity);

      return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id)
    {
      var userEntity = await _service.GetByIdAsync(id);

      if (userEntity == null)
        return NotFound();

      await _service.DeleteAsync(id);

      return NoContent();
    }
  }
}