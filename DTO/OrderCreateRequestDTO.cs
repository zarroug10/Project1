using web.Entities;

namespace web.DTO;
    public class OrderCreateRequestDTO
    {
        public string? CustomerId { get; set; }
        public string? OrderName { get; set; } = string.Empty;
}
