namespace WebApplicationVisual.DTO;

public class AddSetStatusBlockedRequest
{
    
    public int Id { get; set; }
    public bool Is_blocked { get; set; }

    public AddSetStatusBlockedRequest(int id, bool isBlocked)
    {
        Id = id;
        Is_blocked = isBlocked;
    }
}