using web.DTO;
using web.Entities;
using web.Helpers.Pagination;
using web.Helpers.Params;

namespace web.Interfaces;

    public interface IOrderRepository
    {
        Task<PagedList<Order>> GetOrdersAsync(OrderParams orderParams);

        Task<UsersOrderDTO> GetOrderByIdAsync(Guid orderId);

        Task<bool> OrderExisit(Guid orderId);

        Task CreateOrderAsync(OrderCreateRequestDTO order);
        
        Task<bool> UpdateOrderAsync(OrderCreateRequestDTO order);

        Task DeleteOrderAsync(string? orderId);

        Task<bool> SaveAsync();
}
