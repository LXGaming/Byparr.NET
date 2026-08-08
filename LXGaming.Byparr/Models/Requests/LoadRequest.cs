using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace LXGaming.Byparr.Models.Requests;

// https://github.com/ThePhaseless/Byparr/blob/77020bec0d3fab29975a3b05a7c9a3dc29960a17/src/owui.py#L22
public record LoadRequest {

    [JsonPropertyName("urls")]
    public required ImmutableArray<string> Urls { get; init; }
}