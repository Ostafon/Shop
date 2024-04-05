using System;
using System.Collections.Generic;

public class Order
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public int CustomerId { get; set; }
    public virtual Customer Customer { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; }

    public Order()
    {
        OrderItems = new HashSet<OrderItem>();
    }
}