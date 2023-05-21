namespace WebApplicationVisual.DTO;

public class AddDoctorRequest
{
    public string Name { get; set; }
    public string Speciality { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }

    public AddDoctorRequest(string name, string speciality, string phone, string email)
    {
        Name = name;
        Speciality = speciality;
        Phone = phone;
        Email = email;
    }
}