namespace WebApplicationVisual.DTO;

public class EditExpertViewRequest
{
    public int ServiceId { get; set; }
    public string NewView { get; set; }
    public double NewPriceOfView { get; set; }

    public EditExpertViewRequest(int serviceId, string newView, double newPriceOfView)
    {
        ServiceId = serviceId;
        NewView = newView;
        NewPriceOfView = newPriceOfView;
    }
}