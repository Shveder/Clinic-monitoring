using Npgsql;
using WebApplicationVisual.Interfaces;

namespace WebApplicationVisual.Repositories;

public class ObserverRepository : IObserver
{
    public void ChangePrice(int serviceId, double price)
    {
        const string connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres;";
        var connection = new NpgsqlConnection(connectionString);
        var sr = new SubjectRepository();
        ServicesRepository repository = new ServicesRepository();
        var sql = "UPDATE dentist.services SET price = @price WHERE id = @id";
        var command = new NpgsqlCommand(sql, connection);
        foreach (var subscriber in sr.GetSubscribesList(serviceId))
        {
            sr.Notify(subscriber,"Price of service "+repository.GetServiceById(serviceId).ServiceName +" is changed to " + price, DateTime.Now);
        }
        command.Parameters.AddWithValue("@id", serviceId);
        command.Parameters.AddWithValue("@price", price);
        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }
}