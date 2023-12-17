using System.Text.Json.Serialization;

namespace fStore.Core;
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Role
{
    Customer,
    Admin,
}
