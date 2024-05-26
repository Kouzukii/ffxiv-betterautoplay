using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;

namespace BetterAutoplay;

#pragma warning disable 8618
// ReSharper disable UnusedAutoPropertyAccessor.Local
internal class Service {
    [PluginService] internal static IGameInteropProvider GameInteropProvider { get; private set; }
    [PluginService] internal static IAddonLifecycle AddonLifecycle { get; private set; }
    [PluginService] internal static IPluginLog PluginLog { get; private set; }

    internal static void Initialize(DalamudPluginInterface pluginInterface) {
        pluginInterface.Create<Service>();
    }
}
#pragma warning restore 8618
