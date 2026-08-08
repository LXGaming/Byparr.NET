using System.Collections.Frozen;
using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace LXGaming.Byparr.Models;

// https://github.com/ThePhaseless/Byparr/blob/77020bec0d3fab29975a3b05a7c9a3dc29960a17/src/models.py#L60
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

    /// <remarks>
    /// Added in 3.0.0
    /// </remarks>
    [JsonPropertyName("contentType")]
    public string? ContentType { get; init; }
}