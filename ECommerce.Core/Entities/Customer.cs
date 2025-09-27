namespace ECommerce.Core.Entities;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }

    // Propiedades de navegación (para Entity Framework)
    public ICollection<Order> Orders { get; set; }
}