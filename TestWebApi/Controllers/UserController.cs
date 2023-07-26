using Microsoft.AspNetCore.Mvc;
using TestWebApi.Abstractions;
using TestWebApi.DataTransferObjects;

namespace TestWebApi.Controllers;

[ApiController]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost("user/register")]
    public IActionResult Register([FromBody] UserDto user)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.SelectMany(x => x.Value.Errors.Select(xx => new { Code = x.Key, Message = xx.ErrorMessage })));

        var result = _userService.Register(user.Name, user.Email, user.Password);

        if (result == null)
            return Ok();
        
        return Ok(user);
    }
}