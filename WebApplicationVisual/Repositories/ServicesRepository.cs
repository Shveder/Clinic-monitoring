using Npgsql;
using WebApplicationVisual.Models;

namespace WebApplicationVisual.Repositories;

public class ServicesRepository : RepositoryBase
{
    public List<Service> GetServiceListFromDataBase()
    {
        NpgsqlConnection connection = new NpgsqlConnection(connectionString);
        connection.Open();
        var sql = "SELECT * FROM dentist.services";
        List<Service> services = new List<Service>();
        using (var command = new NpgsqlCommand(sql, connection))
        {
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string serviceName = reader.GetString(1);
                    int doctorId = reader.GetInt32(2);
                    int clinicId = reader.GetInt32(3);
                    double price = reader.GetDouble(4);
                    string view = reader.GetString(5);
                    double priceOfView = reader.GetDouble(6);
                    services.Add(new Service(id, serviceName, doctorId, clinicId, price, view, priceOfView));
                }
            }
        }
        connection.Close();
        return services;
    }

    public Service GetServiceById(int serviceId)
    {
        var connection = new NpgsqlConnection(connectionString);
        var sql = "SELECT * FROM dentist.services WHERE @id = id";
        connection.Open();
        using (var command = new NpgsqlCommand(sql,connection))
        {
            command.Parameters.AddWithValue("@id",serviceId);
            using(var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    int doctorId = reader.GetInt32(2);
                    int clinicId = reader.GetInt32(3);
                    double price = reader.GetDouble(4);
                    string view = reader.GetString(5);
                    double priceOfView = reader.GetDouble(6);
                    connection.Close();
                    return new Service(id, name, doctorId, clinicId, price, view, priceOfView);
                }
            }
        }
        connection.Close();
        return null;
    }
    
    public void DeleteService(int id)
    {
        using var connection = new NpgsqlConnection(connectionString);
        var sql = "DELETE FROM dentist.services WHERE id = @id";
        using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", id);
        OpenConnection(connection);
        command.ExecuteNonQuery();
        CloseConnection(connection);
    }
    
    public void WriteServiceToDataBase(string serviceName, int doctorId, int clinicId, double price, string expertView, double priceOfExpert)
    {
        
        
        var service = new Service(1, serviceName, doctorId, clinicId, price, expertView, priceOfExpert);
        var sql = "INSERT INTO dentist.services (service_name, doctor_id, clinic_id, price, expert_view, expert_view_price) " +
                  "VALUES (@service_name, @doctor_id, @clinic_id, @price, @expertView, @expertViewPrice)";

        using var connection = new NpgsqlConnection(connectionString);
        using var command = new NpgsqlCommand(sql, connection);

        command.Parameters.AddWithValue("@service_name", service.ServiceName);
        command.Parameters.AddWithValue("@doctor_id", service.DoctorId);
        command.Parameters.AddWithValue("@clinic_id", service.ClinicId);
        command.Parameters.AddWithValue("@price", service.Price);
        command.Parameters.AddWithValue("@expertView", service.ExpertView);
        command.Parameters.AddWithValue("@expertViewPrice", service.ExpertViewPrice);
            
        OpenConnection(connection);
        command.ExecuteNonQuery();
        CloseConnection(connection);
        
    }
    

    public void AddRecentPrice(Service service)
    {
        var connection = new NpgsqlConnection(connectionString);
        var sql = "INSERT INTO dentist.recent_prices (price, date, service_id)" +
                  " VALUES (@price, @date, @serviceId)";
        var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("@price", service.Price);
        command.Parameters.AddWithValue("@date", DateTime.Now);
        command.Parameters.AddWithValue("@serviceId", service.Id);
        OpenConnection(connection);
        command.ExecuteNonQuery();
        CloseConnection(connection);
    }
    
    public List<Recent_prices> GetRecentPricesListFromDataBase(int recentPriceId)
    {
        NpgsqlConnection connection = new NpgsqlConnection(connectionString);
        connection.Open();
        var sql = "SELECT * FROM dentist.recent_prices WHERE service_id = @id";
        List<Recent_prices> prices = new List<Recent_prices>();
        using (var command = new NpgsqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@id", recentPriceId);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    double price = reader.GetDouble(1);
                    DateTime date = reader.GetDateTime(2);
                    int serviceId = reader.GetInt32(3);
                    prices.Add(new Recent_prices(id, price, date, serviceId));
                }
            }
        }
        connection.Close();
        return prices;
    }

    public void AddPurchace(int userId, int serviceId, double priceOfView)
    {
        var ar = new AdminRepository();
        var ur = new UserRepository();
        ar.ChangeUserBalance(userId,ur.GetUserById(userId).Balance - priceOfView);
        using var connection = new NpgsqlConnection(connectionString);
        var sql = "INSERT INTO dentist.purchases_of_view  (price, user_id, service_id) VALUES (@price, @user_id, @service_id)";
        using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("@price", priceOfView);
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@service_id", serviceId);
        OpenConnection(connection);
        command.ExecuteNonQuery();
        CloseConnection(connection);
    }

    public bool IsAlreadyBought(int userId, int serviceId)
    {
        using var connection = new NpgsqlConnection(connectionString);
        var sql = "SELECT COUNT(*) FROM dentist.purchases_of_view WHERE user_id = @userId and service_id= @serviceId";
        using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.AddWithValue("@userId", userId);
        command.Parameters.AddWithValue("@serviceId", serviceId);
        OpenConnection(connection);
        int count = Convert.ToInt32(command.ExecuteScalar());
        CloseConnection(connection);
        return Convert.ToBoolean(count);
    }

    public string GetExpertView(int serviceId)
    {
        var connection = new NpgsqlConnection(connectionString);
        var sql = "SELECT expert_view FROM dentist.services WHERE @id = id";
        connection.Open();
        using (var command = new NpgsqlCommand(sql,connection))
        {
            command.Parameters.AddWithValue("@id",serviceId);
            using(var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string view = reader.GetString(0);
                    connection.Close();
                    return view;
                }
            }
        }
        connection.Close();
        return null;
    }
    
    public List<Service> GetServiceListByDoctorId(int doctor_id)
    {
        NpgsqlConnection connection = new NpgsqlConnection(connectionString);
        connection.Open();
        var sql = "SELECT * FROM dentist.services WHERE doctor_id = @doctorId";
        List<Service> services = new List<Service>();
        using (var command = new NpgsqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@doctorId",doctor_id);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string serviceName = reader.GetString(1);
                    int doctorId = reader.GetInt32(2);
                    int clinicId = reader.GetInt32(3);
                    double price = reader.GetDouble(4);
                    string view = reader.GetString(5);
                    double priceOfView = reader.GetDouble(6);
                    services.Add(new Service(id, serviceName, doctorId, clinicId, price, view, priceOfView));
                }
            }
        }
        connection.Close();
        return services;
    }
    public List<Service> GetServiceListByClinicId(int doctor_id)
    {
        NpgsqlConnection connection = new NpgsqlConnection(connectionString);
        connection.Open();
        var sql = "SELECT * FROM dentist.services WHERE clinic_id = @clinicId";
        List<Service> services = new List<Service>();
        using (var command = new NpgsqlCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@clinicId",doctor_id);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string serviceName = reader.GetString(1);
                    int doctorId = reader.GetInt32(2);
                    int clinicId = reader.GetInt32(3);
                    double price = reader.GetDouble(4);
                    string view = reader.GetString(5);
                    double priceOfView = reader.GetDouble(6);
                    services.Add(new Service(id, serviceName, doctorId, clinicId, price, view, priceOfView));
                }
            }
        }
        connection.Close();
        return services;
    }
}