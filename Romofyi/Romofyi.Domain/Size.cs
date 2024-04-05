using System.Collections.Generic;

public class Size
{
    public int SizeId { get; set; }
    public string Name { get; set; }
    public virtual ICollection<ProductSize> ProductSizes { get; set; }

    public Size()
    {
        ProductSizes = new HashSet<ProductSize>();
    }
}