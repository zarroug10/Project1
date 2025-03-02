using System;

namespace web.Entities;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty ;
    public Money? Price { get; set; }
    //the amount of product in store
    public int Amount { get; set; }
    public ICollection<OrderLine> OrderlineItem { get; } = [];
}
