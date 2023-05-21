using Npgsql;

public class RepositoryBase
{
    protected const string connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres;";

    protected void OpenConnection(NpgsqlConnection connection)
    {
        connection.Open();
    }
        
    protected void CloseConnection(NpgsqlConnection connection)
    {
        connection.Close();
    }
}