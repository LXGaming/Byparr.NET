namespace LXGaming.Byparr;

public sealed class ByparrClientOptions {

    public static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(100);

    public required Uri Address { get; set; }

    public string? ApiKey { get; set; }

    public Dictionary<string, string>? AdditionalHeaders { get; set; }

    public TimeSpan Timeout { get; set; } = DefaultTimeout;
}