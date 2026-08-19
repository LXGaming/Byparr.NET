using System.Text.Json.Serialization;

namespace LXGaming.Byparr.Models.Responses;

public record ErrorResponse {

    [JsonPropertyName("detail")]
    public required string Detail { get; init; }
}