using System.Text.Json.Serialization;
using LXGaming.Common.Text.Json.Serialization.Converters;

namespace LXGaming.Byparr.Models;

// https://github.com/microsoft/playwright-python/blob/3b7c24c3e67dc84f7b0eddd0c5fd2ca685705021/playwright/_impl/_api_structures.py#L34
[JsonConverter(typeof(StringEnumConverter<SameSite>))]
public enum SameSite {

    [JsonPropertyName("Lax")]
    Lax,

    [JsonPropertyName("None")]
    None,

    [JsonPropertyName("Strict")]
    Strict
}