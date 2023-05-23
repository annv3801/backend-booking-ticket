using System.Text.Json;
using Application.Common;
using Application.Common.Interfaces;

namespace Infrastructure.Services.Common;
public class JsonSerializerService : IJsonSerializerService
{
    public string Serialize(object? obj, JsonSerializerOptions? options = null)
    {
        return JsonSerializer.Serialize(obj, options ?? Constants.JsonSerializerOptions);
    }

    public T? Deserialize<T>(string str, JsonSerializerOptions? options = null) where T: class
    {
        return JsonSerializer.Deserialize<T>(str, options ?? Constants.JsonSerializerOptions);
    }
}
