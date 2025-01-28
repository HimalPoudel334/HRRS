using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

using Dapper;
namespace HRRS.Persistence.Context;

public class DapperHelper : IDisposable
{
    private readonly string _connectionString;

    public DapperHelper(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
                           ?? throw new InvalidOperationException("Connection string not found.");
    }

    private IDbConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }

    public IEnumerable<T> Query<T>(string sql, object? parameters = null)
    {
        using var connection = CreateConnection();
        return connection.Query<T>(sql, parameters);
    }

    public T QuerySingle<T>(string sql, object? parameters = null)
    {
        using var connection = CreateConnection();
        return connection.QuerySingle<T>(sql, parameters);
    }

    public int Execute(string sql, object? parameters = null)
    {
        using var connection = CreateConnection();
        return connection.Execute(sql, parameters);
    }

    public T ExecuteScalar<T>(string sql, object? parameters = null)
    {
        using var connection = CreateConnection();
        return connection.ExecuteScalar<T>(sql, parameters);
    }


    public IEnumerable<T> QueryStoredProc<T>(string storedProcName, object? parameters = null)
    {
        using var connection = CreateConnection();
        return connection.Query<T>(storedProcName, parameters, commandType: CommandType.StoredProcedure);
    }
    public void Dispose()
    {
        // Cleanup resources if needed
    }
}

