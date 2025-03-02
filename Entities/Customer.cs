using System;

namespace web.Entities;

public class Customer
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public ICollection<Order> Orders { get; set; } = [];
}
