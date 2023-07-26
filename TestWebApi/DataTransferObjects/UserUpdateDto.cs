using System.ComponentModel.DataAnnotations;

namespace TestWebApi.DataTransferObjects;

public class UserUpdateDto
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
}