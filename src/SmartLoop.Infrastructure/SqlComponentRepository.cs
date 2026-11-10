using SmartLoop.Core.Exceptions;
using SmartLoop.Core.Interfaces;
using SmartLoop.Core.Models;
using Microsoft.Data.SqlClient;

namespace SmartLoop.Infrastructure;

public class SqlComponentRepository : IComponentRepository
{
    private const int ForeignKeyViolationNumber = 547;

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
            components.Add(MapComponent(reader));
        }

        return components;
    }

    public async Task<Component?> GetByIdAsync(int id)
    {
        const string sql = "SELECT Id, Name, Type, Unit, MinValue, MaxValue, Description, CreatedAt FROM Components WHERE Id = @id";

        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        await using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", id);

        await using var reader = await command.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            return MapComponent(reader);
        }

        return null;
    }

    public async Task<int> AddAsync(Component component)
    {
        const string sql = """
        INSERT INTO Components (Name, Type, Unit, MinValue, MaxValue, Description)
        OUTPUT INSERTED.Id
        VALUES (@name, @type, @unit, @minValue, @maxValue, @description)
        """;

        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        await using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@name", component.Name);
        command.Parameters.AddWithValue("@type", component.Type.ToString());
        command.Parameters.AddWithValue("@unit", component.Unit);
        command.Parameters.AddWithValue("@minValue", component.MinValue);
        command.Parameters.AddWithValue("@maxValue", component.MaxValue);
        command.Parameters.AddWithValue("@description", (object?)component.Description ?? DBNull.Value);

        var result = await command.ExecuteScalarAsync();
        return Convert.ToInt32(result);
    }

    public async Task UpdateAsync(Component component)
    {
        const string sql = """
        UPDATE Components
        SET Name = @name, Type = @type, Unit = @unit, MinValue = @minValue, MaxValue = @maxValue, Description = @description
        WHERE Id = @id
        """;

        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        await using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", component.Id);
        command.Parameters.AddWithValue("@name", component.Name);
        command.Parameters.AddWithValue("@type", component.Type.ToString());
        command.Parameters.AddWithValue("@unit", component.Unit);
        command.Parameters.AddWithValue("@minValue", component.MinValue);
        command.Parameters.AddWithValue("@maxValue", component.MaxValue);
        command.Parameters.AddWithValue("@description", (object?)component.Description ?? DBNull.Value);

        await command.ExecuteNonQueryAsync();
    }

    public async Task DeleteAsync(int id)
    {
        const string sql = "DELETE FROM Components WHERE Id = @id";

        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        await using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", id);

        try
        {
            await command.ExecuteNonQueryAsync();
        }
        catch (SqlException ex) when (ex.Number == ForeignKeyViolationNumber)
        {
            throw new ComponentInUseException("The component is used in a control system and cannot be deleted.", ex);
        }
    }

    private static Component MapComponent(SqlDataReader reader)
    {
        return new Component
        {
            Id = reader.GetInt32(0),
            Name = reader.GetString(1),
            Type = Enum.Parse<ComponentType>(reader.GetString(2)),
            Unit = reader.GetString(3),
            MinValue = reader.GetDecimal(4),
            MaxValue = reader.GetDecimal(5),
            Description = reader.IsDBNull(6) ? null : reader.GetString(6),
            CreatedAt = reader.GetDateTime(7)
        };
    }
}