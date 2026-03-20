using LXGaming.Byparr.Models.Requests;

namespace LXGaming.Byparr.Models.Commands;

public record RequestGetCommand : CommandBase {

    public override Command Command => Command.RequestGet;

    public required string Url { get; init; }

    public TimeSpan? MaxTimeout { get; init; }

    public override V1Request ToV1Request() {
        return new V1Request {
            Command = Command,
            Url = Url,
            MaxTimeout = (int?) MaxTimeout?.TotalMilliseconds
        };
    }
}