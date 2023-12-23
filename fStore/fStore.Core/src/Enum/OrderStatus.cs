using System.Text.Json.Serialization;

namespace fStore.Core;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OrderStatus
{
    Pending,
    Processing,
    Cancelled,
    Shipped,
    Delivered
}
