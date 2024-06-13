namespace nhlstendencafe.Models;

public class OrderItem
{
    public required Product Product { get; set; }
    public int Quantity { get; set; }
}