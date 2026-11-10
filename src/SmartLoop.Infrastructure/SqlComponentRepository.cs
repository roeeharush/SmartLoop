using SmartLoop.Core.Interfaces;
using SmartLoop.Core.Models;
using Microsoft.Data.SqlClient;

namespace SmartLoop.Infrastructure;

public class SqlComponentRepository : IComponentRepository
{
    private readonly string _connectionString;

    public SqlComponentRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IReadOnlyList<Component>> GetAllAsync()
    {
        const string sql = "SELECT Id, Name, Type, Unit, MinValue, MaxValue, Description, CreatedAt FROM Components ORDER BY Name";

        var components = new List<Component>();

        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        await using var command = new SqlCommand(sql, connection);
        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            components.Add(new Component
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Type = Enum.Parse<ComponentType>(reader.GetString(2)),
                Unit = reader.GetString(3),
                MinValue = reader.GetDecimal(4),
                MaxValue = reader.GetDecimal(5),
                Description = reader.IsDBNull(6) ? null : reader.GetString(6),
                CreatedAt = reader.GetDateTime(7)
            });
        }

        return components;
    }

    public Task<Component?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<int> AddAsync(Component component)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Component component)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}