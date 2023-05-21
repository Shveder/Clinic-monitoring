namespace WebApplicationVisual.Models;

public class Doctor
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Speciality { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }

    public Doctor(int id, string name, string speciality, string phone, string email)
    {
        Id = id;
        Name = name;
        Speciality = speciality;
        Phone = phone;
        Email = email;
    }
}