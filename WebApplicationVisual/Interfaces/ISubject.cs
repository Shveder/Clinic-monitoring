namespace WebApplicationVisual.Interfaces;

public interface ISubject
{
    void Subscribe(int userId,int serviceId);
    void Unsubscribe(int userId,int serviceId);
    void Notify(int userId,string notification,DateTime date);
}