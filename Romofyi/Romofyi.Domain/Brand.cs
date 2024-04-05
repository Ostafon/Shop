using System.Collections.Generic;

public class Brand
{
    public int BrandId { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public virtual ICollection<Product> Products { get; set; }

    public Brand()
    {
        Products = new HashSet<Product>();
    }
}