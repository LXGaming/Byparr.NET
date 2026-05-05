using System.Text.Json;
using System.Text.Json.Serialization;
using NUnit.Framework;

namespace LXGaming.Byparr.Tests;

public class TestByparrClient : ByparrClient {

    public TestByparrClient(ByparrClientOptions options) : base(options) {
        JsonSerializerOptions.UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow;
    }

    protected override async Task<T> DeserializeAsync<T>(HttpResponseMessage response,
        CancellationToken cancellationToken = default) {
        var expectedElement = await base.DeserializeAsync<JsonElement>(response, cancellationToken);
        return Deserialize<T>(expectedElement);
    }

    private T Deserialize<T>(JsonElement expectedElement) {
        Assert.That(expectedElement, Is.Not.Default);

        var expectedString = JsonSerializer.Serialize(expectedElement, JsonSerializerOptions);
        Assert.That(expectedString, Is.Not.Null.And.Not.Empty);

        var actualObject = expectedElement.Deserialize<T>(JsonSerializerOptions);
        Assert.That(actualObject, Is.Not.Null);

        var actualElement = JsonSerializer.SerializeToElement<T>(actualObject!, JsonSerializerOptions);
        Assert.That(actualElement, Is.Not.Default);

        var actualString = JsonSerializer.Serialize(actualElement, JsonSerializerOptions);
        Assert.That(actualString, Is.Not.Null.And.Not.Empty);

        Warn.Unless(actualString, Is.EqualTo(expectedString).IgnoreCase);
        return actualObject!;
    }
}