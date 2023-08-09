namespace Application.Common.Extensions;
public static class DateTimeExtensions
{
    public static string ToFormattedString(this DateTime? date)
    {
        return date?.ToString(Constants.JsonDateTimeFormat) ?? string.Empty;
    }
}
