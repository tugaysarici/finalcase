namespace Oms.Schema;

public class OrderRequest
{
    public string PaymentMethod { get; set; }
    public virtual List<OrderItemRequest> OrderItems { get; set; }
}

public class OrderResponse
{
    public int UserId { get; set; }
    public int OrderNumber { get; set; }
    public string PaymentMethod { get; set; }
    public decimal OrderAmount { get; set; }
    public bool OrderStatus { get; set; }
    public virtual List<OrderItemResponse> OrderItems { get; set; }
}