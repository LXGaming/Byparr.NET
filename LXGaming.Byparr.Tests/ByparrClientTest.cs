using System.Collections.Frozen;
using LXGaming.Byparr.Models;
using LXGaming.Byparr.Models.Commands;
using LXGaming.Byparr.Models.Requests;
using LXGaming.Byparr.Models.Responses;
using LXGaming.Byparr.Utilities;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace LXGaming.Byparr.Tests;

[Parallelizable]
public class ByparrClientTest : IDisposable {

    private static readonly FrozenDictionary<string, Status> Urls = new Dictionary<string, Status> {
        { "https://lxgaming.me/", Status.Ok },
        { "https://github.com/", Status.Ok },
        { "https://www.google.com/", Status.Ok },
        { "http://localhost/", Status.Error }
    }.ToFrozenDictionary();

    private readonly ByparrClient _byparrClient;
    private bool _disposed;

    public ByparrClientTest() {
        var options = new ConfigurationBuilder()
            .AddUserSecrets(typeof(ByparrClientTest).Assembly)
            .Build()
            .Get<ByparrClientOptions>();
        if (options?.Address == null) {
            Assert.Ignore("Address has not been configured for Byparr");
        }

        _byparrClient = new TestByparrClient(options);
    }

    [Test]
    public async Task GetHealthAsync() {
        using var _ = Assert.EnterMultipleScope();
        var response = await _byparrClient.GetHealthAsync();
        Assert.That(response.Message, Is.EqualTo(HealthResponse.WorkingMessage));
        Assert.That(response.IsSuccessStatus(), Is.True);
    }

    [TestCaseSource(nameof(Urls))]
    public async Task SendLoadAsync(KeyValuePair<string, Status> pair) {
        using var _ = Assert.EnterMultipleScope();
        var responses = await _byparrClient.SendAsync(new LoadRequest {
            Urls = [pair.Key]
        });
        Assert.That(responses, Has.Length.EqualTo(1));

        var response = responses.Single();
        Assert.That(response.PageContent, pair.Value == Status.Ok ? Is.Not.Empty : Is.Empty);
        Assert.That(response.Metadata, Is.Not.Empty);
        Assert.That(response.GetSource(), Is.EqualTo(pair.Key));
    }

    [TestCaseSource(nameof(Urls))]
    public async Task SendV1Async(KeyValuePair<string, Status> pair) {
        using var _ = Assert.EnterMultipleScope();
        var task = _byparrClient.SendAsync(new V1Request {
            Command = Command.RequestGet,
            Url = pair.Key
        });

        if (pair.Value == Status.Ok) {
            var response = await task;
            Assert.That(response.Status, Is.EqualTo(Status.Ok));
            Assert.That(response.Solution, Is.Not.Null);
            Assert.That(response.IsSuccessStatus(), Is.True);
        } else {
            await Assert.ThrowsAsync<HttpRequestException>(() => task);
        }
    }

    [TestCaseSource(nameof(Urls))]
    public async Task SendRequestGetCommandAsync(KeyValuePair<string, Status> pair) {
        var task = _byparrClient.SendCommandAsync(new RequestGetCommand {
            Url = pair.Key
        });

        if (pair.Value == Status.Ok) {
            var solution = await task;
            Assert.That(solution.IsSuccessStatus(), Is.True);
        } else {
            await Assert.ThrowsAsync<HttpRequestException>(() => task);
        }
    }

    public void Dispose() {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing) {
        if (_disposed) {
            return;
        }

        _disposed = true;

        if (disposing) {
            _byparrClient.Dispose();
        }
    }
}