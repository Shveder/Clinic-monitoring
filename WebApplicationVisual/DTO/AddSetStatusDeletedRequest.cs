namespace WebApplicationVisual.DTO;

public class AddSetStatusDeletedRequest
{
    public int Id { get; set; }
    public bool Is_deleted { get; set; }

    public AddSetStatusDeletedRequest(int id, bool isDeleted)
    {
        Id = id;
        Is_deleted = isDeleted;
    }
}