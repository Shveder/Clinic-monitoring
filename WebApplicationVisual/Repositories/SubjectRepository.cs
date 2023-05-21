using Npgsql;
using WebApplicationVisual.Interfaces;
using WebApplicationVisual.Models;

namespace WebApplicationVisual.Repositories;

public class SubjectRepository : ISubject 
{
    const string connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres;";
    
    public void Subscribe(int userId, int serviceId)
    {
        using var connection = new NpgsqlConnection(connectionString);
       
        var sql = "INSERT INTO dentist.subscriptions (user_id, service_id) " +
                  "VALUES (@userId, @serviceId)";

        using var command = new NpgsqlCommand(sql, connection);
        
        command.Parameters.AddWithValue("@userId", userId);
        command.Parameters.AddWithValue("@serviceId", serviceId);
        
        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }

    public void Unsubscribe(int userId, int serviceId)
    {
        using var connection = new NpgsqlConnection(connectionString);
        var sql = "DELETE FROM dentist.subscriptions WHERE user_id = @id and service_id = @serviceId";
        using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", userId);
        command.Parameters.AddWithValue("@serviceId", serviceId);
        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }

    public void Notify(int userId,string notification,DateTime date)
    {
        using var connection = new NpgsqlConnection(connectionString);
       
        var sql = "INSERT INTO dentist.notifications (text, user_id,date) " +
                  "VALUES (@notification, @userId,@date)";

        using var command = new NpgsqlCommand(sql, connection);
        
        command.Parameters.AddWithValue("@notification", notification);
        command.Parameters.AddWithValue("@userId", userId);
        command.Parameters.AddWithValue("@date", date);
        
        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }

    public List<int> GetSubscribesList(int serviceId)
    {
        NpgsqlConnection connection = new NpgsqlConnection(connectionString);
        List<int> subscribers = new List<int>();
        connection.Open();
        var sql = "SELECT user_id FROM dentist.subscriptions WHERE service_id = @user_id";
        using (var command = new NpgsqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("user_id", serviceId);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    subscribers.Add(id);
                }
            }
        }
        connection.Close();
        return subscribers;
    }

    public void DeleteNotificationById(int id)
    {
        using var connection = new NpgsqlConnection(connectionString);
        var sql = "DELETE FROM dentist.notifications WHERE id = @id ";
        using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", id);
        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }
    public List<Notification> GetNotificationsList(int userId)
    {
        NpgsqlConnection connection = new NpgsqlConnection(connectionString);
        List<Notification> notifications = new List<Notification>();
        connection.Open();
        var sql = "SELECT * FROM dentist.notifications WHERE user_id = @user_id";
        using (var command = new NpgsqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("user_id", userId);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string text = reader.GetString(1);
                    DateTime date = reader.GetDateTime(3);
                    int userID = reader.GetInt32(2);
                    
                    
                    notifications.Add(new Notification(id, text, date, userId));
                }
            }
        }
        connection.Close();
        return notifications;
    }

    public int GetNumberOfNotifications(int userId)
    {
        NpgsqlConnection connection = new NpgsqlConnection(connectionString);
        connection.Open();
        var sql = "SELECT COUNT(*) FROM dentist.notifications WHERE user_id = @user_id";
        int count;
        using (var command = new NpgsqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("user_id", userId);
            count = Convert.ToInt32(command.ExecuteScalar());
            
        }
        connection.Close();
        return count;
    }
}