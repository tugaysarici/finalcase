namespace Oms.Schema;

public class ProductRequest
{
    public string Name { get; set; }
    public decimal BasePrice { get; set; }
    public int Stock {  get; set; }
    public string Description { get; set; }
    public int MinimumQuantity { get; set; }
   
}

public class ProductResponse
{
    public string Name { get; set; }
    public decimal BasePrice { get; set; }
    public int Stock { get; set; }
    public string Description { get; set; }
    public int MinimumQuantity { get; set; }
}