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

    private static readonly string[] Urls = [
        "https://lxgaming.me/",
        "https://github.com/",
        "https://www.google.com/"
    ];

    private readonly ByparrClient _byparrClient;
    private bool _disposed;

    public ByparrClientTest() {
        var options = new ConfigurationBuilder()
            .AddUserSecrets(typeof(ByparrClientTest).Assembly)
            .Build()
            .Get<ByparrClientOptions>();
        if (options?.BaseAddress == null) {
            Assert.Ignore("BaseAddress has not been configured for Byparr");
        }

        _byparrClient = new TestByparrClient(options);
    }

    [Test]
    public async Task GetHealthAsync() {
        var response = await _byparrClient.GetHealthAsync();
        using var _ = Assert.EnterMultipleScope();
        Assert.That(response.Message, Is.EqualTo(HealthResponse.WorkingMessage));
        Assert.That(response.IsSuccessStatus(), Is.True);
    }

    [TestCaseSource(nameof(Urls))]
    public async Task SendAsync(string url) {
        var response = await _byparrClient.SendAsync(new V1Request {
            Command = Command.RequestGet,
            Url = url
        });
        using var _ = Assert.EnterMultipleScope();
        Assert.That(response.Status, Is.EqualTo(Status.Ok));
        Assert.That(response.Solution, Is.Not.Null);
        Assert.That(response.IsSuccessStatus(), Is.True);
    }

    [TestCaseSource(nameof(Urls))]
    [Order(3)]
    public async Task SendRequestGetCommandAsync(string url) {
        await _byparrClient.SendCommandAsync(new RequestGetCommand {
            Url = url
        });
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