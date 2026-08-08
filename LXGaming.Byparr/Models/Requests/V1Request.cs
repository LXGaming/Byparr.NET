using System.Text.Json.Serialization;

namespace LXGaming.Byparr.Models.Requests;

// https://github.com/ThePhaseless/Byparr/blob/77020bec0d3fab29975a3b05a7c9a3dc29960a17/src/models.py#L16
public record V1Request {

    [JsonPropertyName("cmd")]
    public required Command Command { get; init; }

    [JsonPropertyName("url")]
    public string? Url { get; init; }

    [JsonPropertyName("maxTimeout")]
    public int? MaxTimeout { get; init; }

    [JsonPropertyName("blockMedia")]
    public bool? BlockMedia { get; init; }

    [JsonPropertyName("returnOnlyCookies")]
    public bool? ReturnOnlyCookies { get; init; }
}