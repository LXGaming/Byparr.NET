using System.Text.Json.Serialization;
using LXGaming.Common.Text.Json.Serialization.Converters;

namespace LXGaming.Byparr.Models;

[JsonConverter(typeof(StringEnumConverter<Status>))]
public enum Status {

    [JsonPropertyName("ok")]
    Ok,

    [JsonPropertyName("error")]
    Error
}