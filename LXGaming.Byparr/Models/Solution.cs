using System.Collections.Frozen;
using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace LXGaming.Byparr.Models;

// https://github.com/ThePhaseless/Byparr/blob/ceef2d66bfd24d8014a5392d5622671584fa9162/src/models.py#L33
public record Solution {

    [JsonPropertyName("url")]
    public required string Url { get; init; }

    [JsonPropertyName("status")]
    public int Status { get; init; }

    [JsonPropertyName("cookies")]
    public required ImmutableArray<Cookie> Cookies { get; init; }

    [JsonPropertyName("userAgent")]
    public required string UserAgent { get; init; }

    [JsonPropertyName("headers")]
    public required FrozenDictionary<string, object> Headers { get; init; }

    [JsonPropertyName("response")]
    public required string Response { get; init; }
}