namespace WebApplicationVisual.Interfaces;

public interface IObserver
{
    public void ChangePrice(int serviceID, double price);
}