using Npgsql;

namespace Infrastructure.Data;

public static class Database
{
    private static string connectionString = "Host=localhost;Port=5432;Database=booknest;Username=postgres;Password=0510";
    public static NpgsqlConnection Connection()=>new NpgsqlConnection(connectionString);
}


