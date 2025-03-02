using web.Entities;

namespace web.DTO;
    public class OrderCreateRequestDTO
    {
        public Guid? CustomerId { get; set; }
        public string? OrderName { get; set; } = string.Empty;
}
