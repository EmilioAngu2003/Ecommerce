namespace Ecommerce.Core.Entities;

public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public string Status { get; set; }
    public decimal TotalAmount { get; set; }

    // Propiedades de navegación
    public Customer Customer { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
}