using System.Text.Json.Serialization;

namespace LXGaming.Byparr.Models.Requests;

// https://github.com/ThePhaseless/Byparr/blob/ceef2d66bfd24d8014a5392d5622671584fa9162/src/models.py#L14
public record V1Request {

    [JsonPropertyName("cmd")]
    public required Command Command { get; init; }

    [JsonPropertyName("url")]
    public string? Url { get; init; }

    [JsonPropertyName("maxTimeout")]
    public int? MaxTimeout { get; init; }
}