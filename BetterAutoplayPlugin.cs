using System;
using Dalamud.Game.Addon.Lifecycle.AddonArgTypes;
using Dalamud.Plugin;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using FFXIVClientStructs.FFXIV.Component.GUI;

namespace BetterAutoplay;

public class BetterAutoplayPlugin : IDalamudPlugin {
    private readonly Hooks hooks;

    private readonly unsafe AgentInterface* cutsceneAgent;

    public BetterAutoplayPlugin(DalamudPluginInterface pluginInterface) {
        Service.Initialize(pluginInterface);

        hooks = new Hooks(this);

        unsafe {
            cutsceneAgent = AgentModule.Instance()->GetAgentByInternalId(AgentId.Cutscene);
            if (cutsceneAgent == null)
                throw new InvalidOperationException("Cutscene Agent may not be null");
        }
    }

    public void Dispose() {
        hooks.Dispose();
    }

    public unsafe void HandleAddonTalkRefresh(AddonRefreshArgs refreshArgs) {
        var autoAdvance = hooks.CutsceneManager_GetAutoAdvance(cutsceneAgent);
        if ((autoAdvance == 1) == (refreshArgs.AtkValueSpan[7].UInt == 0)) {
            hooks.CutsceneManager_ToggleAutoAdvance(cutsceneAgent, 0, true);
        }
    }
}
