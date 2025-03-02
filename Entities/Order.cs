using System;

namespace web.Entities;

public class Order
{
    public Guid Id { get; set; }
    public string OrderName { get; set; } = string.Empty;
    public DateOnly OrderDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public bool IsOrdered { get; set; } = false;
    public Guid CustomerId { get; set; }
    public Customer? User {get; set; }
    public ICollection<OrderLine> OrderlineItem { get; set; } =[];
}