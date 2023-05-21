using System.Diagnostics.Eventing.Reader;
using System.Text.RegularExpressions;
using Npgsql;
using WebApplicationVisual.Models;

namespace WebApplicationVisual.Repositories;

public class AdminRepository : RepositoryBase
{
    public void SetUserStatusDel(int id, bool isDeleted)
    {
        using var connection = new NpgsqlConnection(connectionString);
        var sql = "UPDATE dentist.users SET is_deleted = @isDeleted WHERE id = @id";
        using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", id);
        command.Parameters.AddWithValue("@isDeleted", isDeleted);
        OpenConnection(connection);
        command.ExecuteNonQuery();
        CloseConnection(connection);
    }
        
    public void SetUserStatusBlock(int id, bool isBlocked)
    {
        using var connection = new NpgsqlConnection(connectionString);
        var sql = "UPDATE dentist.users SET is_blocked = @isBlocked WHERE id = @id";
        using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", id);
        command.Parameters.AddWithValue("@isBlocked", isBlocked);
        OpenConnection(connection);
        command.ExecuteNonQuery();
        CloseConnection(connection);
    }
        
    public void DeleteUser(int id)
    {
        using var connection = new NpgsqlConnection(connectionString);
        var sql = "DELETE FROM dentist.users WHERE id = @id";
        using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", id);
        OpenConnection(connection);
        command.ExecuteNonQuery();
        CloseConnection(connection);
    }
    
    public List<User> GetUserList()
    {
        NpgsqlConnection connection = new NpgsqlConnection(connectionString);
        User user;
        connection.Open();
        var sql = "SELECT * FROM dentist.users";
        List<Models.User> users = new List<User>();
        using (var command = new NpgsqlCommand(sql, connection))
        {
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string login = reader.GetString(1);
                    string password = reader.GetString(2);
                    double balance = reader.GetDouble(3);
                    string salt = reader.GetString(4);
                    int role = reader.GetInt32(5);
                    Boolean isBlocked = reader.GetBoolean(6);
                    Boolean isDeleted = reader.GetBoolean(7);
                    users.Add(new User(id, login, password,balance, salt, role, isBlocked, isDeleted));
                }
            }
        }
        connection.Close();
        return users;
    }
    
    public List<Doctor> GetDoctorList()
    {
        NpgsqlConnection connection = new NpgsqlConnection(connectionString);
        connection.Open();
        var sql = "SELECT * FROM dentist.doctors";
        List<Doctor> doctors = new List<Doctor>();
        using (var command = new NpgsqlCommand(sql, connection))
        {
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    string speciality = reader.GetString(2);
                    string phone = reader.GetString(3);
                    string email = reader.GetString(4);
                    doctors.Add(new Doctor(id, name, speciality,phone, email));
                }
            }
        }
        connection.Close();
        return doctors;
    }
    
    /*public void EditUser(int id,User user)
    {
        using var connection = new NpgsqlConnection(connectionString);
        var sql = "UPDATE dentist.users SET (login, password, balance, salt, role, is_deleted, is_blocked) " +
                  "= (@login, @password, @balance, @salt, @role, @is_deleted, @is_blocked) WHERE id = @id";
        using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("@login", user.Login);
        command.Parameters.AddWithValue("@password", user.Password);
        command.Parameters.AddWithValue("@balance", user.Balance);
        command.Parameters.AddWithValue("@salt", user.Salt);
        command.Parameters.AddWithValue("@role", user.Role);
        command.Parameters.AddWithValue("@is_deleted", user.IsDeleted);
        command.Parameters.AddWithValue("@is_blocked", user.IsBlocked);
        command.Parameters.AddWithValue("@id", user.Id);
        
        OpenConnection(connection);
        command.ExecuteNonQuery();
        CloseConnection(connection);
    }*/

    public void WriteNewDoctorToDatabase(string name, string speciality, string phone, string email)
    {
        var doctor = new Doctor(1, name, speciality, phone, email);
        var sql = "INSERT INTO dentist.doctors (name, speciality, phone, email) " +
                  "VALUES (@name, @speciality, @phone, @email)";

        using var connection = new NpgsqlConnection(connectionString);
        using var command = new NpgsqlCommand(sql, connection);

        command.Parameters.AddWithValue("@name", doctor.Name);
        command.Parameters.AddWithValue("@speciality", doctor.Speciality);
        command.Parameters.AddWithValue("@phone", doctor.Phone);
        command.Parameters.AddWithValue("@email", doctor.Email);

        OpenConnection(connection);
        command.ExecuteNonQuery();
        CloseConnection(connection);
    }
    
    public void DeleteDoctor(int id)
    {
        using var connection = new NpgsqlConnection(connectionString);
        var sql = "DELETE FROM dentist.doctors WHERE id = @id";
        using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", id);
        OpenConnection(connection);
        command.ExecuteNonQuery();
        CloseConnection(connection);
    }
    
    public void WriteNewClinicToDatabase(string name, string address, string email)
    {
        var clinic = new Clinic(1, name, address, email);
        var sql = "INSERT INTO dentist.clinics (name, address, email) " +
                  "VALUES (@name, @address, @email)";

        using var connection = new NpgsqlConnection(connectionString);
        using var command = new NpgsqlCommand(sql, connection);

        command.Parameters.AddWithValue("@name", clinic.Name);
        command.Parameters.AddWithValue("@address", clinic.Address);
        command.Parameters.AddWithValue("@email", clinic.Email);

        OpenConnection(connection);
        command.ExecuteNonQuery();
        CloseConnection(connection);
    }
    
    public void DeleteClinic(int id)
    {
        using var connection = new NpgsqlConnection(connectionString);
        var sql = "DELETE FROM dentist.clinics WHERE id = @id";
        using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", id);
        OpenConnection(connection);
        command.ExecuteNonQuery();
        CloseConnection(connection);
    }
    
    public List<Clinic> GetClinicList()
    {
        NpgsqlConnection connection = new NpgsqlConnection(connectionString);
        connection.Open();
        var sql = "SELECT * FROM dentist.clinics";
        List<Clinic> clinics = new List<Clinic>();
        using (var command = new NpgsqlCommand(sql, connection))
        {
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    string address = reader.GetString(2);
                    string email = reader.GetString(3);
                    clinics.Add(new Clinic(id, name, address, email));
                }
            }
        }
        connection.Close();
        return clinics;
    }
    
    public bool IsThereThisDoctor(int doctorId)
    {
        NpgsqlConnection connection = new NpgsqlConnection(connectionString);
        var sql = "SELECT COUNT(*) FROM dentist.doctors WHERE id = @doctorId";
        var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("doctorId", doctorId);
        OpenConnection(connection);
        int count = Convert.ToInt32(command.ExecuteScalar());
        CloseConnection(connection);
        return Convert.ToBoolean(count);
    }
    public bool IsThereThisClinic(int clinicId)
    {
        NpgsqlConnection connection = new NpgsqlConnection(connectionString);
        var sql = "SELECT COUNT(*) FROM dentist.clinics WHERE id = @clinicId";
        var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("clinicId", clinicId);
        OpenConnection(connection);
        int count = Convert.ToInt32(command.ExecuteScalar());
        CloseConnection(connection);
        return Convert.ToBoolean(count);
    }
    public void ChangeUserRole(int id, int role)
    {
        using var connection = new NpgsqlConnection(connectionString);
        var sql = "UPDATE dentist.users SET role = @role WHERE id = @id";
        using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", id);
        command.Parameters.AddWithValue("@role", role);
        OpenConnection(connection);
        command.ExecuteNonQuery();
        CloseConnection(connection);
    }
    public void ChangeUserBalance(int id, double balance)
    {
        using var connection = new NpgsqlConnection(connectionString);
        var sql = "UPDATE dentist.users SET balance = @balance WHERE id = @id";
        using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", id);
        command.Parameters.AddWithValue("@balance", balance);
        OpenConnection(connection);
        command.ExecuteNonQuery();
        CloseConnection(connection);
    }
    public List<LoginHistory> GetLoginHistory(int userId)
    {
        NpgsqlConnection connection = new NpgsqlConnection(connectionString);
        List<LoginHistory> loginHistories = new List<LoginHistory>();
        var sql = "SELECT * FROM dentist.login_history WHERE user_id = @userID ";
        OpenConnection(connection);
        using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@userId", userId);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string ip = reader.GetString(1);
                    DateTime date = reader.GetDateTime(2);
                    int user_id = reader.GetInt32(3);
                    loginHistories.Add(new LoginHistory(id ,ip, date, user_id));
                }
            }
        }
        CloseConnection(connection);
        return loginHistories;
    }

    public void EditExpertView(int id, string view, double price)
    {
        DeletePurchases(id);
        using var connection = new NpgsqlConnection(connectionString);
        var sql = "UPDATE dentist.services SET (expert_view,expert_view_price) " +
                  "= (@view,@price) WHERE id = @id";
        using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", id);
        command.Parameters.AddWithValue("@view", view);
        command.Parameters.AddWithValue("@price", price);
        OpenConnection(connection);
        command.ExecuteNonQuery();
        CloseConnection(connection);
    }

    public void DeletePurchases(int serviceId)
    {
        using var connection = new NpgsqlConnection(connectionString);
        var sql = "DELETE FROM dentist.purchases_of_view WHERE service_id = @id";
        using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", serviceId);
        OpenConnection(connection);
        command.ExecuteNonQuery();
        CloseConnection(connection);
    }

    public Doctor GetDoctorByID(int doctorId)
    {
        NpgsqlConnection connection = new NpgsqlConnection(connectionString);
        Doctor doctor;
        connection.Open();
        var sql = "SELECT * FROM dentist.doctors WHERE id = @doctorId";
        using (var command = new NpgsqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("doctorId", doctorId);
                
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    string speciality = reader.GetString(2);
                    string phone = reader.GetString(3);
                    string email = reader.GetString(4);
                    doctor = new Doctor(id, name, speciality,phone, email);
                    connection.Close();
                    return doctor;
                }
            }
        }
        connection.Close(); // закрываем подключение
        return null;
    }
    
    public Clinic GetClinicByID(int clinicId)
    {
        NpgsqlConnection connection = new NpgsqlConnection(connectionString);
        Clinic clinic;
        connection.Open();
        var sql = "SELECT * FROM dentist.clinics WHERE id = @clinicId";
        using (var command = new NpgsqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("clinicId", clinicId);
                
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    string address = reader.GetString(2);
                    string email = reader.GetString(3);
                    clinic = new Clinic(id, name, address, email);
                    connection.Close();
                    return clinic;
                }
            }
        }
        connection.Close(); // закрываем подключение
        return null;
    }

    public string GetDoctorNameById(int id)
    {
        NpgsqlConnection connection = new NpgsqlConnection(connectionString);
        connection.Open();
        var sql = "SELECT name FROM dentist.doctors WHERE id = @doctorId";
        using (var command = new NpgsqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("doctorId", id);
                
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string name = reader.GetString(0);
                    connection.Close();
                    return name;
                }
            }
        }
        connection.Close(); // закрываем подключение
        return null;
    }
    public string GetClinicNameById(int id)
    {
        NpgsqlConnection connection = new NpgsqlConnection(connectionString);
        connection.Open();
        var sql = "SELECT name FROM dentist.clinics WHERE id = @clinicId";
        using (var command = new NpgsqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("clinicId", id);
                
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string name = reader.GetString(0);
                    connection.Close();
                    return name;
                }
            }
        }
        connection.Close(); // закрываем подключение
        return null;
    }

    public List<Deposit> GetDepositsOfUser(int userId)
    {
        NpgsqlConnection connection = new NpgsqlConnection(connectionString);
        List<Deposit> deposits = new List<Deposit>();
        connection.Open();
        var sql = "SELECT * FROM dentist.deposits WHERE user_id = @userId";
        using (var command = new NpgsqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("userId", userId);
                
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    double sum = reader.GetDouble(1);
                    DateTime date = reader.GetDateTime(2);
                    int userID = reader.GetInt32(3);
                    deposits.Add(new Deposit(id, sum, date, userID));
                }
            }
        }
        connection.Close(); // закрываем подключение
        return deposits;
        
    }
    
    public List<Purchase>GetPurchasesOfUser(int userId)
    {
        NpgsqlConnection connection = new NpgsqlConnection(connectionString);
        List<Purchase> purchases = new List<Purchase>();
        connection.Open();
        var sql = "SELECT * FROM dentist.purchases_of_view WHERE user_id = @userId";
        using (var command = new NpgsqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("userId", userId);
                
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    double price = reader.GetDouble(1);
                    int userID = reader.GetInt32(2);
                    int serviceID = reader.GetInt32(3);
                    purchases.Add(new Purchase(id, price, userID, serviceID));
                }
            }
        }
        connection.Close(); // закрываем подключение
        return purchases;
        
    }
}