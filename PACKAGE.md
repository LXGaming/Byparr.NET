# Byparr.NET

[![License](https://img.shields.io/github/license/LXGaming/Byparr.NET?label=License&cacheSeconds=86400)](https://github.com/LXGaming/Byparr.NET/blob/main/LICENSE)
[![NuGet](https://img.shields.io/nuget/vpre/LXGaming.Byparr?label=NuGet)](https://www.nuget.org/packages/LXGaming.Byparr)

**Byparr.NET** is an unofficial open source [.NET](https://dotnet.microsoft.com/) library for [Byparr](https://github.com/ThePhaseless/Byparr).

## Usage
### ByparrClient
```csharp
using var client = new ByparrClient(new ByparrClientOptions {
    BaseAddress = new Uri("http://localhost:8191/")
});

// For rawdogging Byparr, you're responsible for setting the appropriate properties.
V1Response response1 = await client.SendAsync(new V1Request {
    Command = Command.RequestGet,
    Url = "https://github.com/LXGaming/Byparr.NET"
});
response1.EnsureSuccessStatus();
response1.Solution.EnsureSuccessStatus();
Console.WriteLine(response1.Solution!.Response);

// For safe Byparring, we've curated the properties just for you.
Solution solution = await client.SendCommandAsync(new RequestGetCommand {
    Url = "https://github.com/LXGaming/Byparr.NET",
    // We even handle those pesky arbitrary values.
    MaxTimeout = TimeSpan.FromMinutes(1)
});
// And we only return what you need.
solution.EnsureSuccessStatus();
Console.WriteLine(solution.Response);

// The best of both worlds, safe requests and raw responses.
V1Response response2 = await client.SendAsync(new RequestGetCommand {
    Url = "https://github.com/LXGaming/Byparr.NET"
}.ToV1Request()); // The secret sauce ;)
response2.EnsureSuccessStatus();
response2.Solution.EnsureSuccessStatus();
Console.WriteLine(response2.Solution!.Response);
```

## License
Byparr.NET is licensed under the [Apache 2.0](https://github.com/LXGaming/Byparr.NET/blob/main/LICENSE) license.