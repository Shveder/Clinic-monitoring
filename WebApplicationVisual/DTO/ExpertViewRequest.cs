namespace WebApplicationVisual.DTO;

public class ExpertViewRequest
{
    public int UserId { get; set; }
    public int ServiceId { get; set; }

    public ExpertViewRequest(int userId, int serviceId)
    {
        UserId = userId;
        ServiceId = serviceId;
    }
}