using System.Text.Json;

namespace Application.Common.Interfaces;
public interface IJsonSerializerService
{
    public string Serialize(object? obj, JsonSerializerOptions? options = null);
    public T? Deserialize<T>(string str, JsonSerializerOptions? options = null) where T: class;
}
