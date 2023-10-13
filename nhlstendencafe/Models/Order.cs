namespace nhlstendencafe.Models;

public class Order
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int TableNumber { get; set; }
    public Guid WaiterId { get; set; }
    public List<OrderItem> Items { get; set; }
    public decimal TotalPrice { get; set; }

    public Order()
    {
        Items = new List<OrderItem>();
    }
}

