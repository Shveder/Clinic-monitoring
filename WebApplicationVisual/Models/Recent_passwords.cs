namespace WebApplicationVisual.Models;

public class RecentPasswords
{
    private int Id { get; set; }
    public string Password { get; set; }
    private int User_id { get; set; }
   
    public RecentPasswords(int id, string password, int userId)
    {
        Id = id;
        Password = password;
        User_id = userId;
    }
    
}