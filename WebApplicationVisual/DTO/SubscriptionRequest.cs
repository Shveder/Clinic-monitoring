namespace WebApplicationVisual.DTO;

public class SubscriptionRequest
{
    public int UserId { get; set; }
    public int ServiceId { get; set; }

    public SubscriptionRequest(int userId, int serviceId)
    {
        UserId = userId;
        ServiceId = serviceId;
    }
}