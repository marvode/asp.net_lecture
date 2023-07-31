using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using TestWebApi.Abstractions;
using TestWebApi.Constants;
using TestWebApi.CustomAttributes;
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

        return Ok(result);
    }
    
    [HttpPost("user/login")]
    public IActionResult Login([FromBody] LoginDto userCredentials)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.SelectMany(x => x.Value.Errors.Select(xx => new { Code = x.Key, Message = xx.ErrorMessage })));

        var result = _userService.Login(userCredentials.Email, userCredentials.Password);

        return Ok(result);
    }
    
    [Authorize(UserRole.User)]
    [HttpPut("user/edit")]
    public IActionResult Edit([FromBody] UserUpdateDto userEdit)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.SelectMany(x => x.Value.Errors.Select(xx => new { Code = x.Key, Message = xx.ErrorMessage })));

        var identity = HttpContext.User.Identity as ClaimsIdentity;
        var claim = identity.FindFirst(ClaimTypes.NameIdentifier);
        var userId = claim.Value;

        var fullName = $"{userEdit.FirstName} {userEdit.LastName}";
        var result = _userService.EditUserName(userId, fullName);

        return Ok(result);
    }

    [HttpGet("user")]
    public IActionResult GetAll([FromQuery] PaginationRequestDto pageInfo)
    {
        return Ok(_userService.GetAll(pageInfo.PageSize, pageInfo.PageNumber));
    }
}