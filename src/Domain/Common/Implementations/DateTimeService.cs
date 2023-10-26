using Application.Interface;

namespace Application.Implementations;

public class DateTimeService : IDateTimeService
{
    public DateTimeOffset Now => DateTimeOffset.Now;
    public DateTimeOffset NowUtc => DateTimeOffset.UtcNow;

    public DateTimeOffset NowWithOffset(TimeSpan offset)
    {
        var now = DateTime.Now;
        return new DateTimeOffset(now.Year, now.Month, now.Day, now.Hour,
            now.Minute, now.Second, now.Millisecond, offset);
    }
}