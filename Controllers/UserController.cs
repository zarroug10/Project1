using System;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

using web.Data;
using web.Entities;
using web.Interfaces;
using web.DTO;
using web.Helpers.Pagination.Extensions;
using web.Helpers.Params;

namespace web.Controllers;



[Route("api/[controller]")]
public class UserController(IUserRepository userRepository,IMapper mapper,CustomDbcontext context) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetUsers ([FromQuery] UserParams user)
    {
        var users = await userRepository.GetUsers(user);
        Response.AddPaginationResponseHeader(users);
        return Ok(users);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUserById (string? userId)
    {
        var user = await userRepository.GetUserById(userId);

        if  (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpGet("users/{username}")]
    public async Task<IActionResult> GetUserByUsername( string username)
    {
        var user = await userRepository.GetUserByUsername(username);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterRequestDTO userDto)
    {
        try
        {
            var userExists = await userRepository.UserExist(userDto.UserName);

            if (userExists)
            {
                return BadRequest("User Already Exist");
            }

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                                       .SelectMany(v => v.Errors)
                                       .Select(e => e.ErrorMessage)
                                       .ToList();

                return BadRequest(errors);
            }

            var user = await userRepository.CreateUser(userDto);

            return Ok(User);
        }
        catch (Exception ex)
        {
            return StatusCode(statusCode: 500, ex.Message);
        }
    }

    [HttpPatch("{customerId}")]
    public async Task<IActionResult> UpdateUser(string? customerId, [FromBody] UpdateRequest updateRequest)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                                       .SelectMany(v => v.Errors)
                                       .Select(e => e.ErrorMessage)
                                       .ToList();
                return BadRequest(errors);
            }

            var user = await userRepository.GetUserById(customerId);
            if (user == null)
            {
                return NotFound();
            }

            if(string.IsNullOrEmpty(updateRequest.UserName) || string.IsNullOrEmpty(updateRequest.Email))
            {
                return BadRequest("null or empty Fields are not allowed !");
            }

            mapper.Map(updateRequest, user);

            context.Entry(user).State = EntityState.Modified;

            if (!await userRepository.SaveAsync())
            {
                return BadRequest("Failed to update user");
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUser([FromQuery] string? userId)
    {
        try 
        {
            var deletedUser = await userRepository.DeleteUser(userId);

            if (!deletedUser)
            {
                return BadRequest("Failed to delete user");
            }
            return Ok("User Deleted successfully !");
        }
        catch(Exception ex)
        {
            return StatusCode(500, new {message ="Error While Deleting" });
        }
    }
}
