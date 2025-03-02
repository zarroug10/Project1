using System;

namespace web.Entities;

public class OrderLine
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public Order Order { get; set; } = null!;
    public Product Product { get; set; }= null!;
}
