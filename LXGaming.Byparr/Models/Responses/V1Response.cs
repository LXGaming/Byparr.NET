using System.Text.Json.Serialization;

namespace LXGaming.Byparr.Models.Responses;

// https://github.com/ThePhaseless/Byparr/blob/77020bec0d3fab29975a3b05a7c9a3dc29960a17/src/models.py#L71
public record V1Response {

    [JsonPropertyName("status")]
    public required Status Status { get; init; }

    [JsonPropertyName("message")]
    public required string Message { get; init; }

    [JsonPropertyName("solution")]
    public required Solution Solution { get; init; }

    [JsonPropertyName("startTimestamp")]
    public long StartTimestamp { get; init; }

    [JsonPropertyName("endTimestamp")]
    public long EndTimestamp { get; init; }

    [JsonPropertyName("version")]
    public required string Version { get; init; }
}