namespace BackendFinance.Dal.Settings;

public record DalOptions
{
    public required string PostgresConnectionString { get; init; }

    public required string RedisConnectionString { get; init; }
}