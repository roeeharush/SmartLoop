using Microsoft.Data.SqlClient;
using SmartLoop.Core.Interfaces;
using SmartLoop.Core.Models;

namespace SmartLoop.Infrastructure;

public class SqlSystemRepository : ISystemRepository
{
    private readonly string _connectionString;

    public SqlSystemRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<int> AddAsync(ControlSystem system)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        await using var transaction = (SqlTransaction)await connection.BeginTransactionAsync();

        try
        {
            const string insertSystemSql = """
            INSERT INTO ControlSystems (Name, Description)
            OUTPUT INSERTED.Id
            VALUES (@name, @description)
            """;

            int systemId;

            await using (var command = new SqlCommand(insertSystemSql, connection, transaction))
            {
                command.Parameters.AddWithValue("@name", system.Name);
                command.Parameters.AddWithValue("@description", (object?)system.Description ?? DBNull.Value);

                var result = await command.ExecuteScalarAsync();
                systemId = Convert.ToInt32(result);
            }

            const string insertComponentSql = """
            INSERT INTO SystemComponents (SystemId, ComponentId, Role, Position)
            VALUES (@systemId, @componentId, @role, @position)
            """;

            foreach (var item in system.Components)
            {
                await using var command = new SqlCommand(insertComponentSql, connection, transaction);
                command.Parameters.AddWithValue("@systemId", systemId);
                command.Parameters.AddWithValue("@componentId", item.ComponentId);
                command.Parameters.AddWithValue("@role", item.Role.ToString());
                command.Parameters.AddWithValue("@position", item.Position);

                await command.ExecuteNonQueryAsync();
            }

            await transaction.CommitAsync();
            return systemId;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<IReadOnlyList<ControlSystem>> GetAllAsync()
    {
        const string systemsSql = "SELECT Id, Name, Description, CreatedAt FROM ControlSystems ORDER BY Name";
        const string componentsSql = "SELECT SystemId, ComponentId, Role, Position FROM SystemComponents ORDER BY SystemId, Position";

        var systems = new List<ControlSystem>();
        var systemsById = new Dictionary<int, ControlSystem>();

        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();

        await using (var command = new SqlCommand(systemsSql, connection))
        await using (var reader = await command.ExecuteReaderAsync())
        {
            while (await reader.ReadAsync())
            {
                var system = new ControlSystem
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                    CreatedAt = reader.GetDateTime(3)
                };

                systems.Add(system);
                systemsById[system.Id] = system;
            }
        }

        await using (var command = new SqlCommand(componentsSql, connection))
        await using (var reader = await command.ExecuteReaderAsync())
        {
            while (await reader.ReadAsync())
            {
                var systemId = reader.GetInt32(0);

                if (systemsById.TryGetValue(systemId, out var system))
                {
                    system.Components.Add(new SystemComponent
                    {
                        ComponentId = reader.GetInt32(1),
                        Role = Enum.Parse<ComponentType>(reader.GetString(2)),
                        Position = reader.GetInt32(3)
                    });
                }
            }
        }

        return systems;
    }
}