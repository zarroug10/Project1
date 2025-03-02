using System.Collections.Generic;


using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web.Data;
using web.DTO;
using web.Entities;
using web.Helpers.Pagination;
using web.Helpers.Params;
using web.Interfaces;

namespace web.Data.Repository;
public class OrderRepository(CustomDbcontext context, IMapper mapper,ILogger<OrderRepository> logger) : IOrderRepository
{
    #region Linq To Objects
    public async Task CreateOrderAsync(OrderCreateRequestDTO order)
    {
        try
        {
            var orders = mapper.Map<Order>(order);

             context.Orders.Add(orders);
        }
        catch (Exception ex)
        {
           logger.LogError(ex.Message);
        }
    }

    public async Task DeleteOrderAsync(string? orderId)
    {
        var order = await context.Orders.FindAsync(orderId);
        if (order == null)
        {
            logger.LogError("Order Not found");
        }

        context.Orders.Remove(order);
    }

    #endregion

    #region Linq To Entities
    public async Task<UsersOrderDTO> GetOrderByIdAsync(Guid orderId)
    {
        var order = await context.Orders.Where(x => x.Id == orderId)
                                        .Include(x => x.User)
                                        .FirstOrDefaultAsync();

        var orderdto = mapper.Map<UsersOrderDTO>(order);

        return orderdto;
    }

    public async Task<PagedList<Order>> GetOrdersAsync(OrderParams orderParams)
    {
        var ordersQuery = context.Orders.OrderBy(x => x.Id);

        var pagedOrders = await PagedList<Order>.CreateAsync(ordersQuery, orderParams.PageNumber, orderParams.PageSize);

        var orderListDTO = ordersQuery.ToList();

        return new PagedList<Order>
                (
                    orderListDTO,
                    pagedOrders.TotalCount,
                    pagedOrders.CurrentPage,
                    pagedOrders.PageSize
                );
    }

    public async Task<bool> SaveAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateOrderAsync(OrderCreateRequestDTO order)
    {
        context.Orders.Update(mapper.Map<Order>(order));
        return await SaveAsync();
    }
    #endregion

    public async Task<bool> OrderExisit(Guid orderId)
    {
        return await context.Orders.AnyAsync(x => x.Id == orderId);
    }
}
