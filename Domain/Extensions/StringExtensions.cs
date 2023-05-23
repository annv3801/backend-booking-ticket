namespace Domain.Extensions;
public static class StringExtensions
{
    public static bool IsMissing(this string? value)
    {
        return string.IsNullOrWhiteSpace(value);
    }

    public static bool IsPresent(this string value)
    {
        return !string.IsNullOrWhiteSpace(value);
    }
    public static string AddQueryString(this string url, string query)
    {
        if (!url.Contains("?"))
        {
            url += "?";
        }
        else if (!url.EndsWith("&"))
        {
            url += "&";
        }

        return url + query;
    }
}
