namespace WebApplicationVisual.Models;

public class Purchase
{
    public int Id { get; set; }
    public double Price { get; set; }
    public int UserId { get; set; }
    public int ServiceId { get; set; }

    public Purchase(int id, double price, int userId, int serviceId)
    {
        Id = id;
        Price = price;
        UserId = userId;
        ServiceId = serviceId;
    }
}