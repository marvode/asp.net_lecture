using System.ComponentModel.DataAnnotations;

namespace TestWebApi.DataTransferObjects;

public class UserDto
{
    [Required]
    public string Name { get; set; }
    [Required, EmailAddress]
    public string Email { get; set; }
    [Required, MinLength(8)]
    public string Password { get; set; }
}