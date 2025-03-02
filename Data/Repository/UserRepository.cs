
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using web.Data;
using web.DTO;
using web.Entities;
using web.Helpers.Pagination;
using web.Helpers.Params;
using web.Interfaces;

namespace web.Data.Repository;

public class UserRepository(CustomDbcontext context, IMapper mapper) : IUserRepository
{
    #region Linq To Entity
    public async Task<UserDTO?> GetUserById(Guid? userId)
    {
        var user = await context.Users.Include(x=> x.Orders)
                                    .Where(x => x.Id == userId).FirstOrDefaultAsync();
        var mappedUser = mapper.Map<UserDTO>(user);
        return mappedUser;
    }

    public async Task<UserDTO?> GetUserByUsername(string username)
    {
        var user = await context.Users.Include(x=> x.Orders)
                                .Where(x=> x.Name.ToLower() == username.ToLower()).FirstOrDefaultAsync();
        var mappedUser = mapper.Map<UserDTO>(user);
        return mappedUser;
    }

    public async Task<PagedList<UserDTO>> GetUsers(UserParams userParams)
    {

        var Filteredusers = context.Users.OrderBy(x => x.Id)
                                      .Where(p=> p.Name.Contains(userParams.Search) || p.Email.Contains(userParams.Search))
                                      .ProjectTo<UserDTO>(mapper.ConfigurationProvider);

        var userQuery = context.Users.OrderBy(x => x.Id)
                                     .ProjectTo<UserDTO>(mapper.ConfigurationProvider);

        var pagedFilteredUsers = await PagedList<UserDTO>.CreateAsync(Filteredusers, userParams.PageNumber, userParams.PageSize);
        var pagedusers = await PagedList<UserDTO>.CreateAsync(userQuery, userParams.PageNumber, userParams.PageSize);

        return string.IsNullOrEmpty(userParams.Search) ? pagedusers : pagedFilteredUsers; 
    }

    #endregion

    #region Linq To Objects
    public async Task<bool> CreateUser(RegisterRequestDTO registerDto)
    {
        var user = mapper.Map<Customer>(registerDto);
        await context.Users.AddAsync(user);
        return await SaveAsync();
    }

    public async Task<bool> SaveAsync()
    {
        var saved = await context.SaveChangesAsync();
        return saved > 0;
    }

    public async Task<bool> DeleteUser(string customerId)
    {
        var customer = await context.Users.FindAsync(customerId);

        if (customer == null) return false;

        context.Users.Remove(customer);
        return await SaveAsync();
    }

    #endregion
    public async Task<bool> UserExist(string username)
    {
        return await context.Users.AnyAsync(c => c.Name.ToLower() == username.ToLower());
    }
}
