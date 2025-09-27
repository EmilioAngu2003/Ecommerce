namespace ECommerce.Core.Entities;

public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    // Propiedades de navegación
    public Order Order { get; set; }

    // Opcionalmente, puedes tener una referencia al producto
    // público Product Product { get; set; }
}