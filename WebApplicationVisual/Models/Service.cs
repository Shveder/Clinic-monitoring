namespace WebApplicationVisual.Models;

public class Service
{
    public int Id { get; set; }
    public string ServiceName { get; set; }
    public int DoctorId { get; set; }
    public int ClinicId { get; set; }
    public double Price { get; set; }
    public string ExpertView { get; set; }
    public double ExpertViewPrice { get; set; }

    public Service(int id, string serviceName, int doctorId, int clinicId, double price, string expertView, double expertViewPrice)
    {
        Id = id;
        ServiceName = serviceName;
        DoctorId = doctorId;
        ClinicId = clinicId;
        Price = price;
        ExpertView = expertView;
        ExpertViewPrice = expertViewPrice;
    }
}