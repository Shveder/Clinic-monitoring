namespace WebApplicationVisual.Models;

public class Recent_prices
{
    public int Id { get; set; }
    public double Price { get; set; }
    public DateTime Date { get; set; }
    public int ServiceId { get; set; }

    public Recent_prices(int id, double price, DateTime date, int serviceId)
    {
        Id = id;
        Price = price;
        Date = date;
        ServiceId = serviceId;
    }
}