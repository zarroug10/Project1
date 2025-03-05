using System;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using AutoMapper.QueryableExtensions;

using web.Data;
using web.Interfaces;
using web.Helpers.Pagination.Extensions;
using web.DTO;
using web.Helpers.Pagination;
using web.Helpers.Params;
using web.Entities;

namespace web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController(IUserRepository userRepository
    , IOrderRepository orderRepository
    , CustomDbcontext context,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetOrders([FromQuery] OrderParams orderParams, CancellationToken cancellationToken)
    {
        var orders = await orderRepository.GetOrdersAsync(orderParams);

        Response.AddPaginationResponseHeader(orders);
        return Ok(orders);
    }

    [HttpGet("orders/{orderId}")]
    public async Task<IActionResult> GetOrderbyId(Guid orderId, CancellationToken cancellationToken)
    {
        if (!await orderRepository.OrderExisit(orderId))
        {
            return NotFound();
        }
        var order = await orderRepository.GetOrderByIdAsync((orderId));
        if (order == null)
        {
            return NotFound();
        }

        return Ok(order);
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetOrderByUser(string? userId, [FromQuery] UsersOrderDTO userParams)
    {
        try
        {
            var user = await userRepository.GetUserById(userId ?? userParams.CustomerId);

            if (user == null)
            {
                return NotFound("User Not Found");
            }

            var orderQuery = context.Orders
                .Where(x => x.CustomerId == user.Id)
                .ProjectTo<UsersOrderDTO>(mapper.ConfigurationProvider);

            var pagedOrders = await PagedList<UsersOrderDTO>.CreateAsync(orderQuery, userParams.PageNumber, userParams.PageSize);

            return Ok(pagedOrders);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("Stat")]
    public async Task<IActionResult> OrderStatistics()
    {
        try
        {
            var orders = await context.Orders.OrderBy(x => x.Id)
                                             .ToListAsync();
            if (orders.Count == 0)
            {
                return Ok(0);
            }
            var orderCount = (decimal)orders.Count();

            return Ok(orderCount);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("orders/Create")]
    public async Task<IActionResult> CreateOrder([FromBody] OrderCreateRequestDTO orderDTO)
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
            var user = await userRepository.GetUserById(orderDTO.CustomerId);
            //var user = await context.Users.Where(x=> x.Id == FindFirstValue(ClaimsType.NameIdentifier))
            //                               .SingleOrDefault();

            if (user == null) return BadRequest("User is Not found");

            var order = orderRepository.CreateOrderAsync(orderDTO);

            await userRepository.SaveAsync();

            return Ok("Order Created with Success");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.InnerException.Message);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteOrder([FromQuery] string? orderId)
    {
        try
        {
            var order = await context.Orders.Where(x => x.Id.ToString() == orderId)
                                            .FirstOrDefaultAsync();

            if (order == null) return BadRequest("Order Not Found");

            orderRepository.DeleteOrder(order);

            if (await userRepository.SaveAsync())
            {
                return Ok();
            };

            return BadRequest();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Delivered(Guid? orderId)
    {
        await orderRepository.UpdateOrderStatus(orderId);
        return Ok(await orderRepository.SaveAsync());
    }
}
