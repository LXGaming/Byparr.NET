using LXGaming.Byparr.Models.Requests;

namespace LXGaming.Byparr.Models.Commands;

public abstract record CommandBase {

    public abstract Command Command { get; }

    public Proxy? Proxy { get; init; }

    public abstract V1Request ToV1Request();
}