public class OrderItem
{
    public int OrderItemId { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public int SizeId { get; set; }
    public virtual Order Order { get; set; }
    public virtual Product Product { get; set; }
    public virtual Size Size { get; set; }
}