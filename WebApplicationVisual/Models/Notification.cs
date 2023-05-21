namespace WebApplicationVisual.Models;

public class Notification
{
    public int Id { get; set; }
    public string NotificationBody { get; set; }
    public DateTime DateTime { get; set; }
    public int UserID { get; set; }

    public Notification(int id, string notificationBody, DateTime dateTime, int userId)
    {
        Id = id;
        NotificationBody = notificationBody;
        DateTime = dateTime;
        UserID = userId;
    }
}