using Microsoft.Extensions.Diagnostics.HealthChecks;
using Npgsql;

namespace API.HealthChecks;

public class DbHealthCheck(IConfiguration configuration) : IHealthCheck
{
    private readonly string? _connectionString = configuration.GetConnectionString("Default");

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);

                await using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT 1;";
                    await command.ExecuteScalarAsync(cancellationToken);
                }
            }

            return HealthCheckResult.Healthy();
        }
        catch (Exception e)
        {
            return HealthCheckResult.Degraded(exception: e);
        }
    }
}