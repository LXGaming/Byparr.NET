using System.Text.Json.Serialization;

namespace LXGaming.Byparr.Models;

// https://github.com/microsoft/playwright-python/blob/3b7c24c3e67dc84f7b0eddd0c5fd2ca685705021/playwright/_impl/_api_structures.py#L26
public record Cookie {

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("value")]
    public required string Value { get; init; }

    [JsonPropertyName("domain")]
    public required string Domain { get; init; }

    [JsonPropertyName("path")]
    public required string Path { get; init; }

    [JsonPropertyName("expires")]
    public double Expires { get; init; }

    [JsonPropertyName("httpOnly")]
    public bool HttpOnly { get; init; }

    [JsonPropertyName("secure")]
    public bool Secure { get; init; }

    [JsonPropertyName("sameSite")]
    public required SameSite SameSite { get; init; }

    [JsonPropertyName("partitionKey")]
    public string? PartitionKey { get; init; }
}