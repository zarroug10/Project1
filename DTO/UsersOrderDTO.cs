using web.Entities;
using web.Helpers.Pagination;

namespace web.DTO
{
    public class UsersOrderDTO:PaginationParams
    {
        public string? Id { get; set; }
        public string OrderName { get; set; } = string.Empty;
        public DateOnly OrderDate { get; set; }
        public bool isDelivered { get; set; } 
        public string? CustomerId { get; set; }
        public ICollection<OrderLine> OrderlineItem { get; set; } = [];
    }
}
