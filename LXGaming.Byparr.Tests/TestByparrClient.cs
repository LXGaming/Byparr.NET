using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using NUnit.Framework;

namespace LXGaming.Byparr.Tests;

public class TestByparrClient : ByparrClient {

    public TestByparrClient(ByparrClientOptions options) : base(options) {
        JsonSerializerOptions.UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow;
    }

    protected override async Task<T> DeserializeAsync<T>(HttpResponseMessage response,
        CancellationToken cancellationToken = default) {
        var expectedNode = await base.DeserializeAsync<JsonNode>(response, cancellationToken);
        return Deserialize<T>(expectedNode);
    }

    private T Deserialize<T>(JsonNode expectedNode) {
        Assert.That(expectedNode, Is.Not.Null);

        var actualObject = expectedNode.Deserialize<T>(JsonSerializerOptions);
        Assert.That(actualObject, Is.Not.Null);

        var actualNode = JsonSerializer.SerializeToNode<T>(actualObject!, JsonSerializerOptions);
        Assert.That(actualNode, Is.Not.Null);

        Warn.Unless(actualNode!.ToJsonString(), Is.EqualTo(expectedNode.ToJsonString()).IgnoreCase);
        return actualObject!;
    }
}