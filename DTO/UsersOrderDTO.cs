using web.Entities;

namespace web.DTO
{
    public class UsersOrderDTO
    {
        public string OrderName { get; set; } = string.Empty;
        public DateOnly OrderDate { get; set; }
        public bool IsOrdered { get; set; } 
        public Guid CustomerId { get; set; }
        public ICollection<OrderLine> OrderlineItem { get; set; } = [];
    }
}
