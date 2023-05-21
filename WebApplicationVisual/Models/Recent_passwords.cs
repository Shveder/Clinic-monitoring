namespace WebApplicationVisual.Models;

public class RecentPasswords
{
    private int _id { get; set; }
    public string _password { get; set; }
    private int _user_id { get; set; }
   
    public RecentPasswords(int id, string password, int userId)
    {
        _id = id;
        _password = password;
        _user_id = userId;
    }
    
}