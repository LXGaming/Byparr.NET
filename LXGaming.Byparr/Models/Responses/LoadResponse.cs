using System.Collections.Frozen;
using System.Text.Json.Serialization;

namespace LXGaming.Byparr.Models.Responses;

// https://github.com/ThePhaseless/Byparr/blob/77020bec0d3fab29975a3b05a7c9a3dc29960a17/src/owui.py#L26
public record LoadResponse {

    [JsonPropertyName("page_content")]
    public required string PageContent { get; init; }

    [JsonPropertyName("metadata")]
    public required FrozenDictionary<string, string> Metadata { get; init; }
}