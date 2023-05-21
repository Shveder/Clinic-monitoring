namespace WebApplicationVisual.DTO;

public class EditUserRequest
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public double Balance { get; set; }
    public int Role { get; set; }

    public EditUserRequest(int id, string login, string password, double balance, int role)
    {
        Id = id;
        Login = login;
        Password = password;
        Balance = balance;
        Role = role;
    }
}