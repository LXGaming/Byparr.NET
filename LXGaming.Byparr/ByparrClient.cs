using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using LXGaming.Byparr.Models;
using LXGaming.Byparr.Models.Commands;
using LXGaming.Byparr.Models.Requests;
using LXGaming.Byparr.Models.Responses;
using LXGaming.Byparr.Utilities;
using LXGaming.Common.Text.Json;
using LXGaming.Common.Text.Json.Serialization.Converters;

namespace LXGaming.Byparr;

public class ByparrClient : IDisposable {

    protected JsonSerializerOptions JsonSerializerOptions { get; }

    private readonly HttpClient _httpClient;
    private bool _disposed;

    public ByparrClient(ByparrClientOptions options) {
        var handler = new SocketsHttpHandler();
        try {
            handler.AutomaticDecompression = options.AutomaticDecompression;
            handler.PooledConnectionLifetime = options.PooledConnectionLifetime;
            handler.Proxy = options.Proxy;
            handler.UseCookies = false;
        } catch (Exception) {
            handler.Dispose();
            throw;
        }

        _httpClient = new HttpClient(handler);
        try {
            _httpClient.BaseAddress = options.BaseAddress;
            _httpClient.Timeout = options.Timeout;
            _httpClient.DefaultRequestHeaders.Add("User-Agent", Constants.Library.UserAgent);

            if (options.AdditionalHeaders != null) {
                foreach (var pair in options.AdditionalHeaders) {
                    _httpClient.DefaultRequestHeaders.Add(pair.Key, pair.Value);
                }
            }
        } catch (Exception) {
            _httpClient.Dispose();
            throw;
        }

        JsonSerializerOptions = new JsonSerializerOptions {
            Converters = {
                new FrozenDictionaryConverterFactory()
            },
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            TypeInfoResolver = new DefaultJsonTypeInfoResolver()
                .WithOrderPropertiesModifier()
                .WithRequiredPropertiesModifier()
        };
    }

    public async Task<HealthResponse> GetHealthAsync(Proxy? proxy = null,
        CancellationToken cancellationToken = default) {
        using var request = new HttpRequestMessage(HttpMethod.Get, "health");
        using var response = await SendAsync(request, proxy, cancellationToken).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
        return await DeserializeAsync<HealthResponse>(response, cancellationToken).ConfigureAwait(false);
    }

    public async Task<V1Response> SendAsync(V1Request v1Request, Proxy? proxy = null,
        CancellationToken cancellationToken = default) {
        var content = JsonSerializer.Serialize(v1Request, JsonSerializerOptions);
        using var request = new HttpRequestMessage(HttpMethod.Post, "v1");
        request.Content = new StringContent(content, Encoding.UTF8, MediaTypeNames.Application.Json);
        using var response = await SendAsync(request, proxy, cancellationToken).ConfigureAwait(false);
        return await DeserializeAsync<V1Response>(response, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Solution> SendCommandAsync(RequestGetCommand command,
        CancellationToken cancellationToken = default) {
        var response = await SendAsync(command.ToV1Request(), command.Proxy, cancellationToken).ConfigureAwait(false);
        response.EnsureSuccessStatus();
        return response.Solution ?? throw new NullReferenceException("Solution is null.");
    }

    protected virtual async Task<T> DeserializeAsync<T>(HttpResponseMessage response,
        CancellationToken cancellationToken = default) {
        await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
        return await JsonSerializer.DeserializeAsync<T>(stream, JsonSerializerOptions, cancellationToken)
            .ConfigureAwait(false) ?? throw new JsonException($"Failed to deserialize {typeof(T).Name}.");
    }

    protected virtual Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, Proxy? proxy,
        CancellationToken cancellationToken = default) {
        if (proxy != null) {
            request.Headers.Add("X-Proxy-Server", proxy.Server);
            if (proxy.Username != null) {
                request.Headers.Add("X-Proxy-Username", proxy.Username);
            }

            if (proxy.Password != null) {
                request.Headers.Add("X-Proxy-Password", proxy.Password);
            }
        }

        return _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
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
            _httpClient.Dispose();
        }
    }
}