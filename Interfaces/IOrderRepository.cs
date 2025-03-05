using web.DTO;
using web.Entities;
using web.Helpers.Pagination;
using web.Helpers.Params;

namespace web.Interfaces;

    public interface IOrderRepository
    {
        Task<PagedList<UsersOrderDTO>> GetOrdersAsync(OrderParams orderParams);

        Task<UsersOrderDTO> GetOrderByIdAsync(Guid orderId);

        Task<bool> OrderExisit(Guid orderId);

        Task CreateOrderAsync(OrderCreateRequestDTO order);

        Task UpdateOrderStatus(Guid? orderId);

        public void DeleteOrder(Order order);

        Task<bool> SaveAsync();
}
