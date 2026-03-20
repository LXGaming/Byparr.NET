using System.Text.Json.Serialization;

namespace LXGaming.Byparr.Models.Responses;

// https://github.com/ThePhaseless/Byparr/blob/ceef2d66bfd24d8014a5392d5622671584fa9162/src/models.py#L26
public record HealthResponse {

    public const string WorkingMessage = "Byparr is working!";

    [JsonPropertyName("msg")]
    public required string Message { get; init; }

    [JsonPropertyName("version")]
    public required string Version { get; init; }

    [JsonPropertyName("userAgent")]
    public required string UserAgent { get; init; }
}