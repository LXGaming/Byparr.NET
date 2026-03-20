using System.Net;

namespace LXGaming.Byparr;

public sealed class ByparrClientOptions {

    public static readonly TimeSpan DefaultPooledConnectionLifetime = TimeSpan.FromMinutes(5);
    public static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(100);

    public required Uri BaseAddress { get; set; }

    public Dictionary<string, string>? AdditionalHeaders { get; set; }

    public DecompressionMethods AutomaticDecompression { get; set; } = DecompressionMethods.All;

    public TimeSpan PooledConnectionLifetime { get; set; } = DefaultPooledConnectionLifetime;

    public IWebProxy? Proxy { get; set; }

    public TimeSpan Timeout { get; set; } = DefaultTimeout;
}