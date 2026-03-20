using System.Text.Json.Serialization;

namespace LXGaming.Byparr.Models.Responses;

// https://github.com/ThePhaseless/Byparr/blob/ceef2d66bfd24d8014a5392d5622671584fa9162/src/models.py#L43
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