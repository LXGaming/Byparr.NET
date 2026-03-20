namespace LXGaming.Byparr.Models;

public record Proxy {

    public required string Server { get; init; }

    public string? Username { get; init; }

    public string? Password { get; init; }
}