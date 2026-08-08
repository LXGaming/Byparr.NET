using System.Text.Json.Serialization;

namespace LXGaming.Byparr.Models.Responses;

// https://github.com/ThePhaseless/Byparr/blob/77020bec0d3fab29975a3b05a7c9a3dc29960a17/src/models.py#L53
public record HealthResponse {

    public const string WorkingMessage = "Byparr is working!";

    [JsonPropertyName("msg")]
    public required string Message { get; init; }

    [JsonPropertyName("version")]
    public required string Version { get; init; }

    [JsonPropertyName("userAgent")]
    public required string UserAgent { get; init; }
}