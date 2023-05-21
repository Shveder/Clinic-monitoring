namespace WebApplicationVisual.Models;

public class Clinic
{
    public int Id { get; set; }

    public string Name { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }

    public Clinic(int id, string name, string address, string email)
    {
        Id = id;
        Name = name;
        Address = address;
        Email = email;
    }
    
}