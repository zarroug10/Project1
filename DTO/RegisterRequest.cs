using System;
using System.ComponentModel.DataAnnotations;
using web.Helpers.Pagination;

namespace web.DTO;

public class RegisterRequestDTO
{
    [MaxLength(20,ErrorMessage ="UserName can't be longer than 20 characters")]
    public required string UserName { get; set; }

    [EmailAddress]
    public required string Email { get; set; }

    //[StringLength(12,ErrorMessage ="Password must be at least 8 long",MinimumLength =8)]
    //[RegularExpression(@"^(?=.*[A-Z])(?=.*[@&])[A-Za-z\d@&]+$", ErrorMessage = "Password must contain at least one uppercase letter and one special character (@ or &).")]
    //public required string Password { get; set; }

    //[StringLength(8,ErrorMessage ="Cin must be 8 number long")]
    //public required string Cin { get; set; }

    //[StringLength(8,ErrorMessage ="Phone must be 8 number long")]
    //public required string Phone { get; set; }
}
