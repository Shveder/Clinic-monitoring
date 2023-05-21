namespace WebApplicationVisual.DTO;

public class AddClinicRequest
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }

    public AddClinicRequest(string name, string address, string email)
    {
        Name = name;
        Address = address;
        Email = email;
    }

}