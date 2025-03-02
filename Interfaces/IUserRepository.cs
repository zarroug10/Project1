using System;


namespace web.Interfaces;
using web.DTO;
using web.Entities;
using web.Helpers.Pagination;
using web.Helpers.Params;

public interface IUserRepository
{
    Task<PagedList<UserDTO>> GetUsers(UserParams user);
    Task<UserDTO?> GetUserById( Guid? userId);
    Task<UserDTO?> GetUserByUsername( string usernam);
    Task<bool> UserExist(string username);
    Task<bool> CreateUser(RegisterRequestDTO registerDTO);
    Task<bool> DeleteUser(string? userId);
    Task<bool> SaveAsync();
}
