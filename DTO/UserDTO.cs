using System;
using System.ComponentModel.DataAnnotations;
using web.Entities;
using web.Helpers.Pagination;

namespace web.DTO;

public class UserDTO
{
    public Guid Id { get; set; }
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [MaxLength(20, ErrorMessage = "UserName can't be longer than 20 characters")]
    public string Name { get; set; } = string.Empty;
    public List<UsersOrderDTO> Orders { get; set; } = [];
}
