using LXGaming.Common.Utilities;

namespace LXGaming.Byparr.Utilities;

internal static class Constants {

    internal static class Library {

        public const string Name = "Byparr";
        public const string Authors = "Alex Thomson";
        public const string Source = "https://github.com/LXGaming/Byparr.NET";
        public const string Website = "https://lxgaming.me/";

        public static readonly string Version = AssemblyUtils.GetVersion(typeof(Constants).Assembly) ?? "Unknown";
        public static readonly string UserAgent = $"{Name}/{Version} (+{Website})";
    }
}