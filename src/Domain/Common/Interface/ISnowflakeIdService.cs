namespace Application.Interface;

public interface ISnowflakeIdService
{
    Task<long> GenerateId(CancellationToken cancellationToken = default);
    long Max();
    long Min();
}