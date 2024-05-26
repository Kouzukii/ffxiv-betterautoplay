using System;
using System.Diagnostics.CodeAnalysis;
using Dalamud.Game.Addon.Lifecycle;
using Dalamud.Game.Addon.Lifecycle.AddonArgTypes;
using Dalamud.Utility.Signatures;
using FFXIVClientStructs.FFXIV.Component.GUI;

namespace BetterAutoplay;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public class Hooks : IDisposable {
    private readonly BetterAutoplayPlugin plugin;

    [Signature("E8 ?? ?? ?? ?? 84 C0 75 0D B0 01 48 8B 5C 24 ?? 48 83 C4 20 5F C3 83 7B 70 00")]
    public readonly unsafe delegate*unmanaged[Thiscall]<AgentInterface*, byte> CutsceneManager_GetAutoAdvance = null!;

    [Signature("48 89 5C 24 08 48 89 74 24 10 57 48 83 EC 20 48 8B 49 10 41 0F B6 F0")]
    public readonly unsafe delegate*unmanaged[Thiscall]<AgentInterface*, uint, bool, nint> CutsceneManager_ToggleAutoAdvance = null!;

    public Hooks(BetterAutoplayPlugin plugin) {
        this.plugin = plugin;
        Service.GameInteropProvider.InitializeFromAttributes(this);
        Service.AddonLifecycle.RegisterListener(AddonEvent.PreRefresh, "Talk", AddonTalkOnRefresh);
    }

    private void AddonTalkOnRefresh(AddonEvent type, AddonArgs args) {
        if (args is not AddonRefreshArgs refreshArgs)
            return;
        plugin.HandleAddonTalkRefresh(refreshArgs);
    }

    public void Dispose() {
    }
}