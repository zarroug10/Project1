using System.Collections.Generic;


using AutoMapper;
using AutoMapper.QueryableExtensions;
using Azure;
using Microsoft.EntityFrameworkCore;

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

    public void DeleteOrder(Order order)
    {
        try
        {
            context.Orders.Remove(order);
        }
        catch(Exception ex)
        {
            logger.LogError(ex.Message);
        }
    }

    public async Task UpdateOrderStatus(Guid? orderId)
    {
       try
        {
            var order = await context.Orders.FirstOrDefaultAsync(x => x.Id == orderId);
            if (order == null)
            {
                logger.LogError("order Not found");
            }

            var Delivery = order.isDelivered;

            if (Delivery == true)
            {
                logger.LogWarning("the delivery is already Updated");
                throw new Exception("the delivery is already Updated");
            }
            var updatedDate = mapper.Map<Order>(order);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
        }
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

    public async Task<PagedList<UsersOrderDTO>> GetOrdersAsync(OrderParams orderParams)
    {
        var ordersQuery = context.Orders.OrderBy(x => x.Id)
                                        .ProjectTo<UsersOrderDTO>(mapper.ConfigurationProvider);

        var pagedOrders = await PagedList<UsersOrderDTO>.CreateAsync(ordersQuery, orderParams.PageNumber, orderParams.PageSize);

        var orderListDTO = ordersQuery.ToList();

        return new PagedList<UsersOrderDTO>
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

  
    #endregion

    public async Task<bool> OrderExisit(Guid orderId)=>  await context.Orders.AnyAsync(x => x.Id == orderId);
}
