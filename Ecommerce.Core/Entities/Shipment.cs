namespace ECommerce.Core.Entities;

public class Shipment
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string TrackingNumber { get; set; }
    public string Status { get; set; }
    public string ShippingAddress { get; set; }
    public DateTime ShippedAt { get; set; }
    public DateTime DeliveredAt { get; set; }

    // Propiedad de navegación
    public Order Order { get; set; }
}