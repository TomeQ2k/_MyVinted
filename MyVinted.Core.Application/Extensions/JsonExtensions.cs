using System.Text.Json;
using MyVinted.Core.Application.Settings;

namespace MyVinted.Core.Application.Extensions
{
    public static class JsonExtensions
    {
        public static string ToJSON(this object obj) => JsonSerializer.Serialize(obj, options: JsonSettings.JsonSerializerOptions);

        public static T FromJSON<T>(this string obj, JsonSerializerOptions options = null) => JsonSerializer.Deserialize<T>(obj, options: options);
    }
}