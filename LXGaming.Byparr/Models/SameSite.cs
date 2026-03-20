using System.Text.Json.Serialization;
using LXGaming.Common.Text.Json.Serialization.Converters;

namespace LXGaming.Byparr.Models;

// https://github.com/microsoft/playwright-python/blob/47a5d35ef4f815a2021349f86ae391f7c20c02d6/playwright/_impl/_api_structures.py#L34
[JsonConverter(typeof(StringEnumConverter<SameSite>))]
public enum SameSite {

    [JsonPropertyName("Lax")]
    Lax,

    [JsonPropertyName("None")]
    None,

    [JsonPropertyName("Strict")]
    Strict
}