using System.Text.Json.Serialization;
using LXGaming.Common.Text.Json.Serialization.Converters;

namespace LXGaming.Byparr.Models;

[JsonConverter(typeof(StringEnumConverter<Command>))]
public enum Command {

    [JsonPropertyName("request.get")]
    RequestGet
}